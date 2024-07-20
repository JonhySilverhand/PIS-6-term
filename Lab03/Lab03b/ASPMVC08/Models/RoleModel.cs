using Microsoft.AspNetCore.Identity;

namespace ASPCMVC08.Models;

public class RoleModel
{
    public IEnumerable<Role> Users { get; init; }
    public IEnumerable<IdentityRole> Roles { get; init; }

    public RoleModel()
    {
        Users = new List<Role>();
        Roles = new List<IdentityRole>();
    }

    public RoleModel(IEnumerable<Role> users, IEnumerable<IdentityRole> roles)
    {
        Users = users;
        Roles = roles;
    }
}
