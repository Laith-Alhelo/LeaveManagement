using LeaveManagement.Infrastructure.Data;
using LeaveManagement.Infrastructure.Identity;
using LeaveManagement.WebUI.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Application;
using LeaveManagement.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IExceptionLogger, ExceptionLogger>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseExceptionHandling();
app.UseMiddleware<CentralizedExceptionHandling>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
