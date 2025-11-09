using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Users;

namespace OpeningGym.Users.Application.Features.Users.ChangeUserName;
internal class ChangeUserNameCommandHandler : IRequestHandler<ChangeUserNameCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ChangeUserNameCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ChangeUserNameAsync(request.Id, request.NewUserName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
