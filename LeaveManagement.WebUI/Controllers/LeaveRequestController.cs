using LeaveManagement.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create()
        {
            return View(new CreateLeaveRequestViewModel());
        }
    }
}
