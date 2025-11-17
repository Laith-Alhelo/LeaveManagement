using LeaveManagement.Application.Features.Queries;
using LeaveManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Handlers
{
    public class GetAllLeaveRequestsQueryHandler : IRequestHandler<GetAllLeaveRequestsQuery, List<ManagerLeaveRequestDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllLeaveRequestsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ManagerLeaveRequestDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {

            var query = from r in _context.LeaveRequests
                        join u in _context.Users
                            on r.EmployeeId equals u.Id
                        join lt in _context.LeaveTypes
                            on r.LeaveTypeId equals lt.Id
                        select new
                        {
                            r.Id,
                            EmployeeName = u.FirstName + " " + u.LastName,
                            LeaveTypeName = lt.Name,
                            r.StartDate,
                            r.EndDate,
                            r.Status,
                            r.Reason
                        };

            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            query = request.SortOrder switch
            {
                "date_desc" => query.OrderByDescending(x => x.StartDate),
                "status_asc" => query.OrderBy(x => x.Status),
                "status_desc" => query.OrderByDescending(x => x.Status),
                _ => query.OrderBy(x => x.StartDate),
            };

            return await query.Select(x => new ManagerLeaveRequestDto
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
           //     EmployeeName = x.EmployeeId.ToString(),
                LeaveTypeName = x.LeaveTypeName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                Reason = x.Reason
            }).ToListAsync(cancellationToken);
        }
    }
}
