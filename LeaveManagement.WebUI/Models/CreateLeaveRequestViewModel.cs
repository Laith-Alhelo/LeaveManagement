using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.WebUI.Models
{
    public class CreateLeaveRequestViewModel
    {
        [Required]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem>? LeaveTypes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

    }
}
