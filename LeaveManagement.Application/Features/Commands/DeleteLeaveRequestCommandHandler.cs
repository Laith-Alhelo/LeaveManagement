using LeaveManagement.Domain.Enums;
using LeaveManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Commands
{
    public class DeleteLeaveRequestCommandHandler
        : IRequestHandler<DeleteLeaveRequestCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteLeaveRequestCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LeaveRequests
                .FirstOrDefaultAsync(x =>
                        x.Id == request.Id &&
                        x.EmployeeId == request.EmployeeId,
                    cancellationToken);

            if (entity == null)
                return false;

            if (entity.Status != LeaveStatus.Pending)
                return false;

            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
