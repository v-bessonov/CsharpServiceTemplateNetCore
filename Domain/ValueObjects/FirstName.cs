using Domain.Exceptions;
using Domain.Primitives;

namespace Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; }
    
    private FirstName(string value)
    {
        Value = value;
    }

    public static FirstName Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ParameterEmptyDomainException(nameof(FirstName));
        }

        if (firstName.Length > MaxLength)
        {
            throw new MaxLengthDomainException(MaxLength);
        }

        return new FirstName(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}