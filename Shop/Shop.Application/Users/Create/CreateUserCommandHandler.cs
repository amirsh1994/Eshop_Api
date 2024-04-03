using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Create;

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _service;

    public CreateUserCommandHandler(IUserRepository repository, IUserDomainService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = Sha256Hasher.Hash(request.Password);
        var user = new User(request.Name, request.Family, request.PhoneNumber, request.Email, hashedPassword, request.Gender, _service);
        _repository.Add(user);
        await _repository.Save();
        return OperationResult.Success();
    }
}