using MediatR;

namespace OpeningGym.Users.Application.Features.Admins.CreateAdmin;
public sealed record CreateAdminCommand(
    string FullName,
    string Email,
    string Password
    ) : IRequest;
