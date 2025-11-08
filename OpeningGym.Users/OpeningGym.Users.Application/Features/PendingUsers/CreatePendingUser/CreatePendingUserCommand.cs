using MediatR;

namespace OpeningGym.Users.Application.Features.PendingUsers.CreatePendingUser;
public sealed record CreatePendingUserCommand(
    string FullName,
    string Email,
    string Password
    ) : IRequest;
