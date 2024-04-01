﻿using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Edit;

public class EditRoleCommandHandler:IBaseCommandHandler<EditRoleCommand>
{

    private readonly IRoleRepository _repository;

    public EditRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetTracking(request.Id);
        if (role == null)
        {
            return OperationResult.NotFound();
        }
        role.Edit(request.Title);
        var rolePermission = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            rolePermission.Add(new RolePermission(x));
        });
       
        role.SetPermissions(rolePermission);
       
        await _repository.Save();
        return OperationResult.Success();
    }
}