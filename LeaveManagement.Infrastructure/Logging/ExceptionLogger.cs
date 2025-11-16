using LeaveManagement.Domain.Entities;
using LeaveManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Infrastructure.Logging
{
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly ApplicationDbContext _context;

        public ExceptionLogger(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(Exception ex, HttpContext context)
        {
            int? userId = null;

            var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var parsedId))
            {
                userId = parsedId;
            }

            var log = new ExceptionLog
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace ?? string.Empty,
                Path = context.Request.Path,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId?.ToString() ?? "system",
                IsDeleted = false
            };

            _context.ExceptionLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
