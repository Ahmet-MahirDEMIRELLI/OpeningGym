using MediatR;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Users;

namespace OpeningGym.Users.Application.Features.Users.UpdateUserRating;
internal class UpdateUserRatingCommandHandler : IRequestHandler<UpdateUserRatingCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserRatingCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserRatingCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateRatingAsync(request.Id, request.RatingType, request.NewRating, request.NewK, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
