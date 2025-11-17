
using LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Queries
{
    public class LeaveRequestListItemDto
    {
        public int Id { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;
        public string StatusText => Status.ToString();
        public LeaveStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
