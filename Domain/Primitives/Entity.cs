namespace Domain.Primitives;

public abstract class Entity<T>: IEquatable<Entity<T>> where T: notnull
{
    public T Id{ get; private init; }

    protected Entity(T id)
    {
        Id = id;
    }

    public static bool operator ==(Entity<T>? first, Entity<T>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<T>? first, Entity<T>? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }
        
        return EqualityComparer<T>.Default.Equals(other.Id , Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<T> entity)
        {
            return false;
        }
        
        return EqualityComparer<T>.Default.Equals(entity.Id , Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}