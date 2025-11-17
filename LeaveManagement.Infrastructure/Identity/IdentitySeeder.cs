using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            await EnsureRole(roleManager, "Admin");
            await EnsureRole(roleManager, "Manager");
            await EnsureRole(roleManager, "Employee");

            var admin = new ApplicationUser
            {
                UserName = "admin@Emaar.com",
                Email = "admin@Emaar.com",
                FirstName = "Laith",
                LastName = "Admin",
                EmailConfirmed = true
            };
            await EnsureUser(userManager, admin, "Admin@123!", "Admin");

            var manager = new ApplicationUser
            {
                UserName = "layanuhj1233@gmail.com",
                Email = "layanuhj1233@gmail.com",
                FirstName = "layan",
                LastName = "Alhelo",
                EmailConfirmed = true
            };
            await EnsureUser(userManager, manager, "Manager@123!", "Manager");

            var employee = new ApplicationUser
            {
                UserName = "laithhelo13@gmail.com",
                Email = "laithhelo13@gmail.com",
                FirstName = "Yazan",
                LastName = "Alhelo",
                EmailConfirmed = true
            };
            await EnsureUser(userManager, employee, "Employee@123!", "Employee");
        }

        private static async Task EnsureRole(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new ApplicationRole(roleName));
        }

        private static async Task EnsureUser(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password,
            string role)
        {
            var existing = await userManager.FindByEmailAsync(user.Email!);
            if (existing != null)
            {
                if (!await userManager.IsInRoleAsync(existing, role))
                    await userManager.AddToRoleAsync(existing, role);
                return;
            }

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}