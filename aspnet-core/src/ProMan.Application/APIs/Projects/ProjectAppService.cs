using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMan.APIs.ColumnStatuses.Dto;
using ProMan.APIs.Projects.Dto;
using ProMan.Authorization.Users;
using ProMan.Context;
using ProMan.Entities;
using ProMan.Extension;
using ProMan.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

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

            var dicProjectIdToCountUser = Context.GetAll<ProjectUser>()
                .GroupBy(s => s.ProjectId)
                .ToDictionary(s => s.Key, s => s.Select(x => x.UserId).Count());

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
                    CountMember = dicProjectIdToCountUser[s.Id],
                    ClientEmailAddress = s.Customer.EmailAddress
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
            await ValProject(input);
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

        [HttpPost]
        public async Task<CreateEditProjectDto> Edit(CreateEditProjectDto input)
        {
            await ValProject(input);
            var project = await Context.GetAsync<Project>(input.Id);
            ObjectMapper.Map<CreateEditProjectDto, Project>(input, project);
            await Context.GetRepo<Project, long>().UpdateAsync(project);

            // projectUser
            var currentProjectUsers = await Context.GetAll<ProjectUser>()
                .Where(s => s.ProjectId == input.Id)
                .ToListAsync();

            var currentUserIds = currentProjectUsers.Select(s => s.UserId).ToList();

            var newUserIds = input.Users.Select(s => s.UserId).ToList();

            var deleteUserIds = currentUserIds.Except(newUserIds);

            var insertUsers = input.Users.Where(s => !currentUserIds.Contains(s.UserId));

            var deleteProjectUserIds = currentProjectUsers
                .Where(s => !newUserIds.Contains(s.UserId))
                .Select(s => s.Id)
                .ToList();

            var updateUsers = (from cpu in currentProjectUsers
                               join cu in input.Users on cpu.UserId equals cu.UserId
                               select new
                               {
                                   ProjectUser = cpu,
                                   Dto = cu
                               }).ToList();

            foreach(var id in deleteProjectUserIds)
            {
                await Context.DeleteAsync<ProjectUser>(id);
            }

            foreach(var pUserDto in insertUsers)
            {
                var projectUser = ObjectMapper.Map<ProjectUser>(pUserDto);
                projectUser.ProjectId = input.Id;
                await Context.InsertAsync<ProjectUser>(projectUser);
            }

            foreach(var item in updateUsers)
            {
                if(item.Dto.Type != item.ProjectUser.Type)
                {
                    item.ProjectUser.Type = item.Dto.Type;

                    await Context.UpdateAsync<ProjectUser>(item.ProjectUser);
                }
            }
            return input;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task Deactive(EntityDto<long> input)
        {
            var project = await Context.GetAsync<Project>(input.Id);
            if (project != null)
            {
                project.Status = ProjectStatus.Deactive;
                await Context.GetRepo<Project, long>().UpdateAsync(project);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Project is not exist"));
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task Active(EntityDto<long> input)
        {
            var project = await Context.GetAsync<Project>(input.Id);
            if (project != null)
            {
                project.Status = ProjectStatus.Active;
                await Context.UpdateAsync<Project>(project);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Project is not exist"));
            }
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task Delete(EntityDto<long> input)
        {
            var project = await Context.GetAsync<Project>(input.Id);
            if (project != null)
            {
                await Context.GetRepo<Project, long>().DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Project is not exist"));
            }
        }

        [HttpGet]
        public async Task<GetProjectDto> GetOneProject(long id)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            return await Context.GetAll<Project>()
                .Where(s => s.Id == id)
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
                }).FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<List<GetProjectDto>> GetAllProjectNotPaging()
        {
            return await Context.GetAll<Project>()
                .Select(s => new GetProjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Status = s.Status,
                    ClientEmailAddress = s.Customer.EmailAddress,
                }).ToListAsync();
        }
    }
}
