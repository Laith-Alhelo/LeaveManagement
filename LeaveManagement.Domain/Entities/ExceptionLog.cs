using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain.Entities
{
    public class ExceptionLog : BaseEntity
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string? Path { get; set; }
        public int? UserId { get; set; }
    }
}
