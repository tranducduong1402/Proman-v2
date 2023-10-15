using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProMan.EntityFrameworkCore;
using ProMan.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ProMan.Web.Tests
{
    [DependsOn(
        typeof(ProManWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ProManWebTestModule : AbpModule
    {
        public ProManWebTestModule(ProManEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProManWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ProManWebMvcModule).Assembly);
        }
    }
}