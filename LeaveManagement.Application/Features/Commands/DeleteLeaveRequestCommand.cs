using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

    }
}
