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
    [Authorize(Roles = "Employee")]
    public class LeaveRequestController : Controller
    {
        private readonly IMediator mediator;
        public LeaveRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var leaveTypes = await mediator.Send(new GetLeaveTypesListQuery());

            var model = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTypes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.LeaveTypes = await mediator.Send(new GetLeaveTypesListQuery());
                return View(model);
            }

            int employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new CreateLeaveRequestCommand
            {
                EmployeeId = employeeId,
                LeaveTypeId = model.LeaveTypeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason
            };

            await mediator.Send(command);

            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests(LeaveStatus? SelectedStatus, string? SortOrder)
        {
            int employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query = new GetMyLeaveRequestsQuery
            {
                EmployeeId = employeeId,
                Status = SelectedStatus,
                SortOrder = SortOrder
            };

            var requests = await mediator.Send(query);

            var model = new MyLeaveRequestsViewModel
            {
                SelectedStatus = SelectedStatus,
                SortOrder = string.IsNullOrEmpty(SortOrder) ? "date_asc" : SortOrder,
                Requests = requests,
                StatusOptions = new List<SelectListItem>
        {
            new SelectListItem("All", "", !SelectedStatus.HasValue),
            new SelectListItem("Pending", LeaveStatus.Pending.ToString(), SelectedStatus == LeaveStatus.Pending),
            new SelectListItem("Approved", LeaveStatus.Approved.ToString(), SelectedStatus == LeaveStatus.Approved),
            new SelectListItem("Rejected", LeaveStatus.Rejected.ToString(), SelectedStatus == LeaveStatus.Rejected)
        },
                SortOptions = new List<SelectListItem>
        {
            new SelectListItem("Start Date (Oldest First)", "date_asc", SortOrder == "date_asc" || string.IsNullOrEmpty(SortOrder)),
            new SelectListItem("Start Date (Newest First)", "date_desc", SortOrder == "date_desc"),
            new SelectListItem("Status (A–Z)", "status_asc", SortOrder == "status_asc"),
            new SelectListItem("Status (Z–A)", "status_desc", SortOrder == "status_desc")
        }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int EmployeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await mediator.Send(new DeleteLeaveRequestCommand
            {
                Id = id,
                EmployeeId = EmployeeId
            });

         

            return RedirectToAction(nameof(MyRequests));
        }

    }
}
