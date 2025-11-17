using LeaveManagement.Application.Features.Commands;
using LeaveManagement.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Manager")]

    public class AdminUsersController : Controller
    {
        private readonly IMediator _mediator;

        public AdminUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableRoles = new[] { "Admin", "Manager", "Employee" };
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            ViewBag.AvailableRoles = new[] { "Admin", "Manager", "Employee" };

            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateUserCommand
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                Password = model.Password
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError("", "Could not create user. Email may already exist or role is invalid.");
                return View(model);
            }

            TempData["Success"] = "User created successfully and email sent.";
            return RedirectToAction(nameof(Create));
        }
    }
}
