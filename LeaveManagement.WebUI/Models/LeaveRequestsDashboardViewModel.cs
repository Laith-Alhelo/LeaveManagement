using LeaveManagement.Application.Features.Queries;

namespace LeaveManagement.WebUI.Models
{
    public class LeaveRequestsDashboardViewModel
    {
        public IList<ManagerLeaveRequestDto> Requests { get; set; } = new List<ManagerLeaveRequestDto>();

        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int TotalCount => Requests.Count;
    }
}
