using MediatR;

namespace OpeningGym.Users.Application.Features.Users.UpdateUserRating;
public sealed record UpdateUserRatingCommand(
    Guid Id,
    string RatingType,
    int NewRating,
    int NewK
    ) : IRequest;
