using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users;

internal static class UserMapper
{
    public static UserDto Map(this User u)
    {
        return new UserDto()
        {
            Id = u.Id,
            CreationDate = u.CreationDate,
            Password = u.Password,
            AvatarName = u.AvatarName,
            Email = u.Email,
            Family = u.Family,
            Gender = u.Gender,
            Name = u.Name,
            PhoneNumber = u.PhoneNumber,
            IsActive = u.IsActive,
            UserRoles = u.UserRoles.Select(x => new UserRoleDto()
            {
                RoleId = x.RoleId,
                RoleTitle = ""

            }).ToList()

        };

    }

    public static async Task<UserDto> SetUserRoleTitle(this UserDto dto, ShopContext context)
    {
        var roleIds = dto.UserRoles.Select(x => x.RoleId).ToList();

        var result = await context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();
        var userRoleDtos = new List<UserRoleDto>();
        foreach (var role in result)
        {
            userRoleDtos.Add(new UserRoleDto()
            {
                RoleId = role.Id,
                RoleTitle = role.Title
            });
        }
        //result.ForEach(x =>
        //{
        //    userRoleDtos.Add(new UserRoleDto()
        //    {
        //        RoleTitle = x.Title,
        //        RoleId = x.Id,
        //    });
        //});


        dto.UserRoles = userRoleDtos;
        return dto;
    }

    public static UserFilterData MapFilterData(this User u)
    {
        return new UserFilterData()
        {
            Id = u.Id,
            CreationDate = u.CreationDate,
            AvatarName = u.AvatarName,
            Email = u.Email,
            Family = u.Family,
            Gender = u.Gender,
            Name = u.Name,
            PhoneNumber = u.PhoneNumber,
        };
    }
}




