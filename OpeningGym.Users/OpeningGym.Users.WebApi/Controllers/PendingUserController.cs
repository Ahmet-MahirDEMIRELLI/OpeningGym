using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpeningGym.Users.Application.Features.PendingUsers.CreatePendingUser;
using OpeningGym.Users.Application.Features.PendingUsers.VerifyPendingUser;

namespace OpeningGym.Users.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PendingUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public PendingUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreatePendingUserCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPost("Verify")]
    public async Task<IActionResult> Verify(VerifyPendingUserCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
