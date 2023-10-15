using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProMan.Authorization;

namespace ProMan
{
    [DependsOn(
        typeof(ProManCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProManApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProManAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProManApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
