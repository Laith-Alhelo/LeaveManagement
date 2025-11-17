using LeaveManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Queries
{
    public class GetAllLeaveRequestsQuery : IRequest<List<ManagerLeaveRequestDto>>
    {
        public LeaveStatus? Status { get; set; }
        public string? SortOrder { get; set; }
    }

public class ManagerLeaveRequestDto
{
    public int Id { get; set; }
    public string LeaveTypeName { get; set; } = "";
    public string EmployeeName { get; set; } = "";
    public LeaveStatus Status { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartDate { get; set; }
    
    public string? Reason { get; set; }
    public string StatusText => Status.ToString();
}
}
