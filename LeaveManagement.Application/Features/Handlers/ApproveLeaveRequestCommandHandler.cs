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
    public class ApproveLeaveRequestCommandHandler : IRequestHandler<ApproveLeaveRequestCommand, bool>
    {
        private readonly IEmailService emailServicel;
        private readonly ApplicationDbContext _context;
        public ApproveLeaveRequestCommandHandler(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            emailServicel = emailService;
        }

        public async Task<bool> Handle(ApproveLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var user = await _context.Users.Where(u => u.Id == entity.EmployeeId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return false;
            }

            entity.Status = LeaveStatus.Approved;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = request.ManagerId.ToString();
            await _context.SaveChangesAsync(cancellationToken);
            var subject = "Leave Request Approved";
            var body =
                $"Hello {user.FirstName},\n\n" +
                $"Your leave from {entity.StartDate:yyyy-MM-dd} " +
                $"to {entity.EndDate:yyyy-MM-dd} has been Approved.\n\n" +
            "Regards,\nHR";

            await emailServicel.SendAsync(user?.Email, subject, body);

            return true;
        }
    }
}
