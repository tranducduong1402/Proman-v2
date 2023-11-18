using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProMan.Anotations;
using ProMan.Entities;
using ProMan.Paging;
using System;
using System.Collections.Generic;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.Projects.Dto
{
    public class GetAllProjectDto : EntityDto<long>
    {
        [ApplySearch]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime UpdatedAt => LastModifierTime.HasValue ? LastModifierTime.Value : CreationTime;
        public string UpdatedName => String.IsNullOrEmpty(LastModifierUserName) ? CreatedUserName : LastModifierUserName;
        public string CreatedUserName { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModifierTime { get; set; }
        public string PMEmailAddress { get; set; }
        public string ClientEmailAddress { get; set; }
        public int CountMember { get; set; }
    }

    //[AutoMapTo(typeof(Project))]
    //public class GetProjectDto : EntityDto<long>
    //{
    //    public string CustomerName { get; set; }
    //    public string Name { get; set; }
    //    public string Code { get; set; }
    //    public ProjectStatus Status { get; set; }
    //    public List<string> Pms { get; set; }
    //    public int ActiveMember { get; set; }
    //    public DateTime TimeStart { get; set; }
    //    public DateTime? TimeEnd { get; set; }
    //}

    public class InputFilterDto
    {
        public GridParam GridParam { get; set; }
        public ProjectStatus IsActive { get; set; }
    }
}
