namespace Domain.Exceptions;

public class MaxLengthDomainException : DomainException
{
    public MaxLengthDomainException(int maxLength) : base($"String length exceeded {maxLength} chars.")
    {
    }
}