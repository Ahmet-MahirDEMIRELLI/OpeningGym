namespace OpeningGym.Users.Domain.Abstractions;
public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; init; }
    public Entity(Guid id)
    {
        Id = id;
    }

    protected Entity() { }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other is not Entity entity)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return entity.Id == Id;
    }
}
