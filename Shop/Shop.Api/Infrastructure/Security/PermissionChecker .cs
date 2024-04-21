using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Users;

namespace Shop.Api.Infrastructure.Security;

public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly Permission _permission;
    private IUserFacade _userFacade;
    private IRoleFacade _roleFacade;

    public PermissionChecker(Permission permission)
    {
        _permission = permission;
        //_userFacade = userFacade;
        //_roleFacade = roleFacade;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if(HasAllowAnonymous(context))
            return;
        
        _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
        _roleFacade = context.HttpContext.RequestServices.GetRequiredService<IRoleFacade>();

        if (context.HttpContext.User.Identity.IsAuthenticated)//means user has logged in and received token and authenticated
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized");
        }
    }


    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var user = await _userFacade.GetUserById(context.HttpContext.User.GetUserId());

        if (user == null)
            return false;

        var roleIds = user.UserRoles.Select(x => x.RoleId);
        var roles = await _roleFacade.GetRoles();
        var userRole = roles.Where(x => roleIds.Contains(x.Id)).ToList();

        return userRole.Any(x => x.Permissions.Contains(_permission));

    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var item in metaData)
        {
            try
            {
                hasAllowAnonymous = item.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch 
            {
                //ignoreed
            
            }
        }
        return hasAllowAnonymous;
    }
}