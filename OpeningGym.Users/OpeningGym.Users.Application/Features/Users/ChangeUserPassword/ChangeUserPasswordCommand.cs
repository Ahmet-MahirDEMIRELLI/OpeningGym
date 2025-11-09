using MediatR;

namespace OpeningGym.Users.Application.Features.Users.ChangeUserPassword;
public sealed record ChangeUserPasswordCommand(
    Guid Id,
    string NewPassword
    ) : IRequest;
