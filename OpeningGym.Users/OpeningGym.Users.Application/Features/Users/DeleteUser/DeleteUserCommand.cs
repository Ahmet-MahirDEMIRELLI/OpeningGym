using MediatR;

namespace OpeningGym.Users.Application.Features.Users.DeleteUser;
public sealed record DeleteUserCommand(
    Guid Id
    ) : IRequest;
