using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Microsoft.Extensions.Configuration;
using ProMan.Configuration.Dto;
using ProMan.Context;

namespace ProMan.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProManAppServiceBase, IConfigurationAppService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationAppService(IConfiguration configuration, IContext context) : base(context)
        {
            _configuration = configuration;
        }
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
