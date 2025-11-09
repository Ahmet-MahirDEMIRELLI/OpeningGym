using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Users;

namespace OpeningGym.Users.Application.Features.Users.ChangeUserPassword;
internal class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ChangeUserPasswordCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ChangePasswordAsync(request.Id, request.NewPassword, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
