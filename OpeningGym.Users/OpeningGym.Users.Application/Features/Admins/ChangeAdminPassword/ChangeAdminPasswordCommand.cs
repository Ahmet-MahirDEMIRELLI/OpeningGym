using MediatR;

namespace OpeningGym.Users.Application.Features.Admins.ChangeAdminPassword;
public sealed record ChangeAdminPasswordCommand(
    Guid Id,
    string NewPassword
    ) : IRequest;
