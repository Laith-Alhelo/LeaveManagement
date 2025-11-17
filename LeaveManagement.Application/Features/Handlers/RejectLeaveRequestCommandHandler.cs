using LeaveManagement.Application.Common.Interfaces;
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
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        public RejectLeaveRequestCommandHandler(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<bool> Handle(RejectLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            var user = await _context.Users.Where(u => u.Id == entity.EmployeeId).FirstOrDefaultAsync();

            if (entity == null)
            {
                return false;
            }

            entity.Status = LeaveStatus.Rejected;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = request.ManagerId.ToString();
            await _context.SaveChangesAsync(cancellationToken);

            var subject = "Leave Request Rejected";
            var body =
                $"Hello {user.FirstName},\n\n" +
                $"Your leave from {entity.StartDate:yyyy-MM-dd} " +
                $"to {entity.EndDate:yyyy-MM-dd} has been Rejected.\n\n" +
            "Regards,\nHR";

            await _emailService.SendAsync(user?.Email, subject, body);

            return true;
        }
    }
}
