using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Commands
{
    public class CreateLeaveRequestCommand : IRequest<int>
    {
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
