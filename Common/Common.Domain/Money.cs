using Common.Domain;
using Common.Domain.Exceptions;

namespace Common.Domain;

public class Money:ValueObject
{
    public int Value { get; private set; }

    private Money()
    {
        
    }
    public Money(int rialValue)
    {
            if(rialValue<0)
                throw new InvalidDomainDataException(nameof(rialValue));
            this.Value=rialValue;
    }

    public static Money FromRial(int value)
    {
        return new Money(value);
    }
    public static Money FromTooman(int value)
    {
        return new Money(value*10);
    }

    public override string ToString()
    {
        return Value.ToString("#,0");
    }

    public static Money operator +(Money firstMoney,Money secondMoney)
    {
        return new Money(firstMoney.Value+secondMoney.Value);
    }
    public static Money operator -(Money firstMoney, Money secondMoney)
    {
        return new Money(firstMoney.Value - secondMoney.Value);
    }
   
}
