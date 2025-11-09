using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpeningGym.Users.Application.Features.Admins.ChangeAdminPassword;
using OpeningGym.Users.Application.Features.Admins.CreateAdmin;

namespace OpeningGym.Users.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPatch("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangeAdminPasswordCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
