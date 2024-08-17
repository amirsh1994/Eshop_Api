using Common.Application;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;

namespace Shop.Application.Users.EditAddress;

public class EditUserAddressCommand:IBaseCommand
{
    public long Id { get; private set; }

    public long UserId { get;  set; }

    public string Shire { get; private set; }

    public string City { get; private set; }

    public string PostalCode { get; private set; }

    public string PostalAddress { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public string Family { get; private set; }

    public string Name { get; private set; }

    public string NationalCode { get; private set; }

    private EditUserAddressCommand()
    {
        
    }
    public EditUserAddressCommand( string shire, string city, string postalCode, 
        string postalAddress, PhoneNumber phoneNumber, string family, string name, string nationalCode, long addressId, long userId)
    {
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Family = family;
        Name = name;
        NationalCode = nationalCode;
        Id = addressId;
        UserId = userId;
    }
}


//public class EditAddressCommandValidator:AbstractValidator<EditUserAddressCommand>
//{
//    public EditAddressCommandValidator()
//    {
//        RuleFor(f => f.City)
//            .NotEmpty().WithMessage(ValidationMessages.required("شهر"));

//        RuleFor(f => f.Shire)
//            .NotEmpty().WithMessage(ValidationMessages.required("استان"));

//        RuleFor(f => f.Name)
//            .NotEmpty().WithMessage(ValidationMessages.required("نام"));

//        RuleFor(f => f.Family)
//            .NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));

//        RuleFor(f => f.NationalCode)
//            .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
//            .ValidNationalId();

//        RuleFor(f => f.PostalAddress)
//            .NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));

//        RuleFor(f => f.PostalCode)
//            .NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));
//    }
//}