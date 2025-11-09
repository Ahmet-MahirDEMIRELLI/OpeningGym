using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Admins;

namespace OpeningGym.Users.Application.Features.Admins.ChangeAdminPassword;
internal class ChangeAdminPasswordCommandHandler : IRequestHandler<ChangeAdminPasswordCommand>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ChangeAdminPasswordCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
    {
        _adminRepository = adminRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeAdminPasswordCommand request, CancellationToken cancellationToken)
    {
        await _adminRepository.ChangePasswordAsync(request.Id, request.NewPassword, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
