using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.WebUI.Models
{
    public class CreateLeaveRequestViewModel
    {
        [Required]
        public IEnumerable<SelectListItem>? LeaveTypes { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(500)]
        public string reason { get; set; } = string.Empty;

    }
}
