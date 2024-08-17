using Common.Application;
using Common.Application.Validation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Application.Users.Edit;

public class EditUserCommand:IBaseCommand
{
    public long UserId { get;  set; }

    public string Name { get; private set; }

    public string Family { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public Gender Gender { get; private set; }

    public IFormFile? Avatar { get; private set; }

    public EditUserCommand(string name, string family, string phoneNumber, string email, Gender gender, IFormFile? avatar, long userId)
    {
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
        Avatar = avatar;
        UserId = userId;
    }
}