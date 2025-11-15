using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain
{
    public class Manager
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public ICollection<Employee> Emplyees { get; set; }
        public ICollection<WorkLeaveRequest> ApprovedRequests { get; set; }
    }
}
