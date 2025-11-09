using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpeningGym.Users.Application.Features.Users.ChangeUserName;
using OpeningGym.Users.Application.Features.Users.ChangeUserPassword;
using OpeningGym.Users.Application.Features.Users.UpdateUserRating;

namespace OpeningGym.Users.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("ChangeUserName")]
    public async Task<IActionResult> ChangeUserName(ChangeUserNameCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPatch("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPatch("UpdateRating")]
    public async Task<IActionResult> UpdateRating(UpdateUserRatingCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
