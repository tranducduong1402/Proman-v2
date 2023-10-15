using System.Threading.Tasks;
using ProMan.Configuration.Dto;

namespace ProMan.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
