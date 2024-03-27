using Common.Domain.Exceptions;

namespace Common.Domain.Exceptions;

public class InvalidDomainDataException:BaseDomainException
{
    public InvalidDomainDataException()
    {
        
    }

    public InvalidDomainDataException(string message):base(message)
    {
        
    }
}
