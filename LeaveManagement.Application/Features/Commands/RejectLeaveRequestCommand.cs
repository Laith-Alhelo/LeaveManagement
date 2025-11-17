using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Commands
{
    public class RejectLeaveRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
    }
}
