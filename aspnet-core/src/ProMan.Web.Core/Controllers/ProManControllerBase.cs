using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ProMan.Controllers
{
    public abstract class ProManControllerBase: AbpController
    {
        protected ProManControllerBase()
        {
            LocalizationSourceName = ProManConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
