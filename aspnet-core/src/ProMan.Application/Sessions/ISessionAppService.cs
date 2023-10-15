using System.Threading.Tasks;
using Abp.Application.Services;
using ProMan.Sessions.Dto;

namespace ProMan.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
