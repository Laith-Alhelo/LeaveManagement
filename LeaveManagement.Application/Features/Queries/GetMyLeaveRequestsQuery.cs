using LeaveManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Queries
{
    public class GetMyLeaveRequestsQuery : IRequest<List<LeaveRequestListItemDto>>
    {
        public int EmployeeId { get; set; }
        public string? SortOrder { get; set; }
        public LeaveStatus? Status { get; set; }
    }
}
