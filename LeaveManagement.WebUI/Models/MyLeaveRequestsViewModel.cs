using LeaveManagement.Application.Features.Queries;
using LeaveManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagement.WebUI.Models
{
    public class MyLeaveRequestsViewModel
    {
        public LeaveStatus? SelectedStatus { get; set; }
        public string? SortOrder { get; set; }
        public List<LeaveRequestListItemDto> Requests { get; set; } = new();
        public IEnumerable<SelectListItem>? SortOptions { get; set; }
        public IEnumerable<SelectListItem>? StatusOptions { get; set; }
    }
}
