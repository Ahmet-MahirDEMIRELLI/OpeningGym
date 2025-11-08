using MediatR;

namespace OpeningGym.Users.Application.Features.PendingUsers.VerifyPendingUser;
public sealed record VerifyPendingUserCommand(
    string Email,
    string VerificationCode
    ) : IRequest;
