using Common.Application;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public string CurrentPassword { get; set; }

    public string Password { get; set; }

    public long UserId { get; set; }

    private  ChangeUserPasswordCommand()
    {
        
    }

    public ChangeUserPasswordCommand(string currentPassword, string password, long userId)
    {
        CurrentPassword = currentPassword;
        Password = password;
        UserId = userId;
    }
}

public class ChangePasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var hashedCurrentPassword = Sha256Hasher.Hash(request.CurrentPassword);
        if (user.Password != hashedCurrentPassword)
        {
            return OperationResult.Error("کلمه عبور فعلی نا معتبر می باشد");
        }
        var hashedNewPassword = Sha256Hasher.Hash(request.Password);
        user.ChangePassword(hashedNewPassword);
        await _userRepository.Save();
        return OperationResult.Success();

    }
}

public class ChangePasswordCommandHandlerValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangePasswordCommandHandlerValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));
    }
}