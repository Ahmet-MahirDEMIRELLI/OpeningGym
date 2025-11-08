using MediatR;
using OpeningGym.Users.Application.Features.Auth.Services;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.PendingUsers;
using OpeningGym.Users.Domain.PendingUsers.Events;

namespace OpeningGym.Users.Application.Features.PendingUsers.CreatePendingUser;
internal class CreatePendingUserCommandHandler : IRequestHandler<CreatePendingUserCommand>
{
    private readonly IPendingUserRepository _pendingUserRepository;
    private readonly VerificationCodeService _verificationCodeService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public CreatePendingUserCommandHandler(IPendingUserRepository pendingUserRepository, VerificationCodeService verificationCodeService, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _pendingUserRepository = pendingUserRepository;
        _verificationCodeService = verificationCodeService;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(CreatePendingUserCommand request, CancellationToken cancellationToken)
    {
        var verificationCode = _verificationCodeService.GenerateEmailVerificationCode();
        var pendingUser = await _pendingUserRepository.CreateAsync(request.FullName, request.Email, request.Password, verificationCode, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new PendingUserCreatedDomainEvent(pendingUser.UserName, pendingUser.Email, pendingUser.VerificationCode), cancellationToken);
    }
}
