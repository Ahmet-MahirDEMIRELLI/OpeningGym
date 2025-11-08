using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.Users;
public sealed class User : Entity
{

#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private User() { }
#pragma warning restore CS8618
    private User(Guid id, string userName, string email, Password password, Rating rating) : base(id)
    {
        UserName = userName;
        Email = email;
        Password = password;
        Rating = rating;
    }

    public string UserName { get; private set; }
    public string Email { get; init; }
    public Password Password { get; private set; }
    public Rating Rating { get; private set; }
    public static User CreateUser(string userName, string email, string hashedPassword)
    {
        return new User(
            id: Guid.NewGuid(),
            userName: userName,
            email: email,
            password: new(hashedPassword, false),
            rating: new(1500, 40, 1500, 30, 1500, 20, 1500, 10)
        );
    }
    public void ChangeUserName(string userName) => UserName = userName;
    public void ChangePassword(string password) => Password = new(password, true);
    public void UpdateBulletRating(int newRating, int newK) => Rating.UpdateBullet(newRating, newK);
    public void UpdateBlitzRating(int newRating, int newK) => Rating.UpdateBlitz(newRating, newK);
    public void UpdateRapidRating(int newRating, int newK) => Rating.UpdateRapid(newRating, newK);
    public void UpdateClassicalRating(int newRating, int newK) => Rating.UpdateClassical(newRating, newK);
}
