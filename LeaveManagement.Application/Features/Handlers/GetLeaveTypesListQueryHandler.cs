using LeaveManagement.Application.Features.Queries;
using LeaveManagement.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Application.Features.Handlers
{
    public class GetLeaveTypesListQueryHandler
        : IRequestHandler<GetLeaveTypesListQuery, IEnumerable<SelectListItem>>
    {
        private readonly ApplicationDbContext _context;

        public GetLeaveTypesListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> Handle(
            GetLeaveTypesListQuery request, CancellationToken cancellationToken)
        {
            return await _context.LeaveTypes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
