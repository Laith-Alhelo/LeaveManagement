using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Infrastructure.Logging
{
    public interface IExceptionLogger
    {
        Task LogAsync(Exception ex, HttpContext context);
    }
}
