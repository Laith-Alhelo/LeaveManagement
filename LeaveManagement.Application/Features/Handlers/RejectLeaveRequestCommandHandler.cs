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
    public class RejectLeaveRequestCommandHandler : IRequestHandler<RejectLeaveRequestCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        public RejectLeaveRequestCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(RejectLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            entity.Status = LeaveStatus.Rejected;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = request.ManagerId.ToString();
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
