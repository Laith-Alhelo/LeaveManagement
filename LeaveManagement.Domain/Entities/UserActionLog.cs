using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Domain.Entities
{
    public class UserActionLog : BaseEntity
    {
        public required int UserId { get; set; }
        public required string ActionName { get; set; }
        public string? ActionDescription { get; set; }
    }
}
