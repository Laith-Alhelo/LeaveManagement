using LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain.Entities
{
    public class WorkLeaveRequest : BaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        public string? ApprovedById { get; set; }
        public DateTime? DateActioned { get; set; }
        public DateTime DateRequested { get; set; } = DateTime.UtcNow;
        public string? ManagerComments { get; set; }
        public string Reason { get; set; } = null!;
    

    }
}
