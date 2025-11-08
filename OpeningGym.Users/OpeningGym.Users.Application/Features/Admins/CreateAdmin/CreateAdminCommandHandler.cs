using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Admins;
using OpeningGym.Users.Domain.Admins.Events;

namespace OpeningGym.Users.Application.Features.Admins.CreateAdmin;
internal class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public CreateAdminCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _adminRepository = adminRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.CreateAsync(request.FullName, request.Email, request.Password);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new AdminCreatedDomainEvent(admin.FullName, admin.Email), cancellationToken);
    }
}
