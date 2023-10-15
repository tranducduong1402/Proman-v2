using Abp.Authorization;
using ProMan.Authorization.Roles;
using ProMan.Authorization.Users;

namespace ProMan.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
