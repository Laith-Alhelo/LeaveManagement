using LeaveManagement.Application.Features.Commands;
using LeaveManagement.Application.Features.Queries;
using LeaveManagement.Domain.Enums;
using LeaveManagement.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace LeaveManagement.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class LeaveApprovalController : Controller
    {
        private readonly IMediator _mediator;

        public LeaveApprovalController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> AllRequests(LeaveStatus? status, string? sortOrder)
        {
            var requests = await _mediator.Send(new GetAllLeaveRequestsQuery
            {
                Status = status,
                SortOrder = sortOrder
            });


            var dashboardVm = new LeaveRequestsDashboardViewModel
            {
                Requests = requests.ToList(),
                PendingCount = requests.Count(r => r.Status == LeaveStatus.Pending),
                ApprovedCount = requests.Count(r => r.Status == LeaveStatus.Approved),
                RejectedCount = requests.Count(r => r.Status == LeaveStatus.Rejected)
            };

            ViewBag.StatusOptions = new List<SelectListItem>
    {
        new SelectListItem("All",    "", !status.HasValue),
        new SelectListItem("Pending",  LeaveStatus.Pending.ToString(),  status == LeaveStatus.Pending),
        new SelectListItem("Approved", LeaveStatus.Approved.ToString(), status == LeaveStatus.Approved),
        new SelectListItem("Rejected", LeaveStatus.Rejected.ToString(), status == LeaveStatus.Rejected)
    };

            ViewBag.SortOptions = new List<SelectListItem>
    {
        new SelectListItem("Date Asc",     "date_asc",   sortOrder == "date_asc"   || string.IsNullOrEmpty(sortOrder)),
        new SelectListItem("Date Desc",    "date_desc",  sortOrder == "date_desc"),
        new SelectListItem("Status A-Z",   "status_asc", sortOrder == "status_asc"),
        new SelectListItem("Status Z-A",   "status_desc",sortOrder == "status_desc")
    };

            return View(dashboardVm);
        }
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            int managerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _mediator.Send(new ApproveLeaveRequestCommand
            {
                Id = id,
                ManagerId = managerId
            });

            return RedirectToAction("AllRequests");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            int managerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _mediator.Send(new RejectLeaveRequestCommand
            {
                Id = id,
                ManagerId = managerId
            });

            return RedirectToAction("AllRequests");
        }
    }
}
