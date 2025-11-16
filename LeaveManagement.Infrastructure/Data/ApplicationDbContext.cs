
using LeaveManagement.Domain.Entities;
using LeaveManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options) 
        {
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<WorkLeaveRequest> LeaveRequests { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LeaveType>().HasData(
                    new LeaveType
                    {
                        Id = 1,
                        Name = "Annual Leave",
                        DefaultDays = 30,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    },
                    new LeaveType
                    {
                        Id = 2,
                        Name = "Sick Leave",
                        DefaultDays = 1,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    },
                    new LeaveType
                    {
                        Id = 3,
                        Name = "Maternity Leave",
                        DefaultDays = 45,
                        CreatedBy = "system",
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    }
                );
        }

    }
}
