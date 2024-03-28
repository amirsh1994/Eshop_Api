namespace Common.Domain.Exceptions;

public class SlugIsDuplicatedException:BaseDomainException
{
    public SlugIsDuplicatedException():base("تکراری هست  Slug")
    {

    }

    public SlugIsDuplicatedException(string message) : base(message)
    {

    }
}