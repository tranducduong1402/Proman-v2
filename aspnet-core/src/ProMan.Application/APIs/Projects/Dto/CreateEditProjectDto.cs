using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProMan.Entities;
using System;
using System.Collections.Generic;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.Projects.Dto
{
    [AutoMapTo(typeof(Project))]
    public class CreateEditProjectDto : EntityDto<long>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public long CustomerId { get; set; }
        public List<AddUserInProjectDto> Users { get; set; }
    }

    public class AddUserInProjectDto
    {
        public long UserId { get; set; }
        public ProjectUserType Type { get; set; }
    }
}
