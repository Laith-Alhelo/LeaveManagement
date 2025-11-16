using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Features.Queries
{
    public class GetLeaveTypesListQuery : IRequest<IEnumerable<SelectListItem>>
    {
    }
}
