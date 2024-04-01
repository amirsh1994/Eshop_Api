using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{

    private readonly IRoleRepository _repository;

    public CreateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            rolePermission.Add(new RolePermission(x));
        });
        var role = new Role(request.Title, rolePermission);
        _repository.Add(role);
        await _repository.Save();
        return OperationResult.Success();
    }
}