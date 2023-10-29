using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using ProMan.Anotations;
using ProMan.Authorization.Users;

namespace ProMan.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        [ApplySearch]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        [ApplySearch]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        [ApplySearch]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        [ApplySearch]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public bool? IsClient { get; set; }
    }
}
