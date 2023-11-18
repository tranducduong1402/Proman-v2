using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProMan.Authorization.Users;

namespace ProMan.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class GetClientDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
