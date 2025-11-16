
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

    }
}
