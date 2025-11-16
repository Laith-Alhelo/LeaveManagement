using LeaveManagement.Application.Features.Commands;
using LeaveManagement.Application.Features.Queries;
using LeaveManagement.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeaveManagement.WebUI.Controllers
{
    //[Authorize(Roles = "Employee")]
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
    }
}
