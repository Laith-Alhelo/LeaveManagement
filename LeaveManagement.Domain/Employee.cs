using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public Manager Manager { get; set; }
        public int ManagerId { get; set; }
        public DateTime HireDate { get; set; }
        public ICollection<WorkLeaveRequest> WorkLeaveRequests { get; set; }

    }
}
