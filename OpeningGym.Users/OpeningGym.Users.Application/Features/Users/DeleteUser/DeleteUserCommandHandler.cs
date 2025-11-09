using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Users;
using OpeningGym.Users.Domain.Users.Events;

namespace OpeningGym.Users.Application.Features.Users.DeleteUser;
internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var oldUser = await _userRepository.DeleteUserAsync(request.Id, cancellationToken) ?? throw new KeyNotFoundException("Id bilgisi hatalı"); ;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new UserDeletedDomainEvent(oldUser.UserName, oldUser.Email), cancellationToken);
    }
}
