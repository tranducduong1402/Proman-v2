using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMan.APIs.Projects.Dto;
using ProMan.Authorization.Users;
using ProMan.Context;
using ProMan.Entities;
using ProMan.Extension;
using ProMan.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.Projects
{
    public class ProjectAppService : ProManAppServiceBase
    {
        public ProjectAppService(IContext context) : base(context) { }

        [HttpPost]
        public async Task<GridResult<GetProjectDto>> GetAllPaging(GridParam input)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            var query = Context.GetAll<Project>()
                .Where(s => !s.IsDeleted)
                .Select(s => new GetProjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = s.Status,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    LastModifierTime = s.LastModificationTime,
                    CreationTime = s.CreationTime,
                    CreatedUserName = (s.CreatorUserId.HasValue && dicUsers.ContainsKey(s.CreatorUserId.Value)) ? dicUsers[s.CreatorUserId.Value] : "",
                    LastModifierUserName = (s.LastModifierUserId.HasValue && dicUsers.ContainsKey(s.LastModifierUserId.Value)) ? dicUsers[s.LastModifierUserId.Value] : "",
                });

            return await query.GetGridResult(query, input);
        }

        private async System.Threading.Tasks.Task ValProject(CreateEditProjectDto input)
        {
            var isExistName = await Context.GetAll<Project>()
                 .Where(s => s.Name == input.Name).Where(s => s.Id != input.Id).AnyAsync();

            if (isExistName)
                throw new UserFriendlyException(string
                    .Format("Project name {0} already existed", input.Name));
        }

        [HttpPost]
        public async Task<CreateEditProjectDto> Create(CreateEditProjectDto input)
        {
            var project = ObjectMapper.Map<Project>(input);
            input.Id = await Context.GetRepo<Project, long>().InsertAndGetIdAsync(project);
            CurrentUnitOfWork.SaveChanges();

            //insert projectUser
            foreach (var pUserDto in input.Users)
            {
                var projectUser = new ProjectUser
                {
                    ProjectId = input.Id,
                    UserId = pUserDto.UserId,
                    Type = pUserDto.Type,
                };
                await Context.GetRepo<ProjectUser, long>().InsertAsync(projectUser);
            }

            return input;
        }
    }
}
