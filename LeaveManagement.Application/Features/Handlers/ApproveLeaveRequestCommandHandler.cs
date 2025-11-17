using LeaveManagement.Application.Features.Commands;
using LeaveManagement.Domain.Enums;
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
    public class ApproveLeaveRequestCommandHandler : IRequestHandler<ApproveLeaveRequestCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public ApproveLeaveRequestCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApproveLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            entity.Status = LeaveStatus.Approved;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = request.ManagerId.ToString();
            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}
