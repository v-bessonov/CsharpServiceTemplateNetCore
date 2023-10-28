namespace Domain.Exceptions;

public class ParameterEmptyDomainException : DomainException
{
    public ParameterEmptyDomainException(string name) : base($"Parameter {name} is empty")
    {
    }
}