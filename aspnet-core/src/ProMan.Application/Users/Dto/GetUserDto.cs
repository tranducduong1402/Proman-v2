using Abp.Application.Services.Dto;

namespace ProMan.Users.Dto
{
    public class GetUserDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        //public string JobTitle { get; set; }
        //public string UserCode { get; set; }
        //public string AvatarPath { get; set; }
        //public string AvatarFullPath => FileUtils.FullFilePath(AvatarPath);
    }
}
