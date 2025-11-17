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
    public class GetMyLeaveRequestsQueryHandler : IRequestHandler<GetMyLeaveRequestsQuery, List<LeaveRequestListItemDto>>
    {
        private readonly ApplicationDbContext _context;
        public GetMyLeaveRequestsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequestListItemDto>> Handle(GetMyLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.LeaveRequests
                .Include(x => x.LeaveType)
                .Where(x => x.EmployeeId == request.EmployeeId);

            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            query = request.SortOrder switch
            {
                "date_desc" => query.OrderByDescending(x => x.StartDate),
                "status_asc" => query.OrderBy(x => x.Status),
                "status_desc" => query.OrderByDescending(x => x.Status),
                _ => query.OrderBy(x => x.StartDate)
            };

            var list = await query
                .Select(x => new LeaveRequestListItemDto
                {
                    Id = x.Id,
                    LeaveTypeName = x.LeaveType.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status
                })
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
