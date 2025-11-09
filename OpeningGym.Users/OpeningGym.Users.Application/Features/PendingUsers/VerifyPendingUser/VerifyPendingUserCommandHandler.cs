using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.PendingUsers;
using OpeningGym.Users.Domain.PendingUsers.Events.PendingUserVerifiedDomainEvent;

namespace OpeningGym.Users.Application.Features.PendingUsers.VerifyPendingUser;
internal class VerifyPendingUserCommandHandler : IRequestHandler<VerifyPendingUserCommand>
{
    private readonly IPendingUserRepository _pendingUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public VerifyPendingUserCommandHandler(IPendingUserRepository pendingUserRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _pendingUserRepository = pendingUserRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(VerifyPendingUserCommand request, CancellationToken cancellationToken)
    {
        var pendingUser = await _pendingUserRepository.VerifyAsync(request.Email, request.VerificationCode, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new PendingUserVerifiedDomainEvent(pendingUser.UserName, pendingUser.Email), cancellationToken);
    }
}
