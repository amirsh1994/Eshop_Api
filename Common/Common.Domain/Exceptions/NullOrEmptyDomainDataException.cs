using Common.Domain.Exceptions;

namespace Common.Domain.Exceptions;

public class NullOrEmptyDomainDataException:BaseDomainException
{
    public NullOrEmptyDomainDataException()
    {
        
    }

    public NullOrEmptyDomainDataException(string message):base(message)
    {
        
    }

    public static void CheckString(string value,string nameOfField)
    {
        if (string.IsNullOrEmpty(value))
            throw new NullOrEmptyDomainDataException($"{nameOfField} is empty or null");
    }
}
