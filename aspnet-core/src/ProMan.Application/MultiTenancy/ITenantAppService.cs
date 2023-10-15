using Abp.Application.Services;
using ProMan.MultiTenancy.Dto;

namespace ProMan.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

