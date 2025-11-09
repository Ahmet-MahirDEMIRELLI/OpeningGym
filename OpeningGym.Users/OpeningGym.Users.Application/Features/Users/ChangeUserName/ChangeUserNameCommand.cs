using MediatR;

namespace OpeningGym.Users.Application.Features.Users.ChangeUserName;
public sealed record ChangeUserNameCommand(
    Guid Id,
    string NewUserName
    ) : IRequest;
