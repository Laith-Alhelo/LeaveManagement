using LeaveManagement.Application.Features.Commands;
using LeaveManagement.Domain.Entities;
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
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ApplicationDbContext _context;
        public CreateLeaveRequestCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.EndDate < request.StartDate)
            {
                throw new ArgumentException("End date cannot be before start date.");
            }

            var leaveTypeExists = await _context.LeaveTypes
                .AnyAsync(l => l.Id == request.LeaveTypeId, cancellationToken);

            if (!leaveTypeExists)
            {
                throw new ArgumentException("Invalid leave type.");
            }

            var user = _context.Users.Where(u => u.Id == request.EmployeeId).FirstOrDefault();
            var entity = new WorkLeaveRequest
            {
                EmployeeId = request.EmployeeId,
                StartDate = request.StartDate,
                LeaveTypeId = request.LeaveTypeId,
                Reason = request.Reason,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = user?.FirstName +" "+user?.LastName,
                Status = LeaveStatus.Pending,
            };
            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
