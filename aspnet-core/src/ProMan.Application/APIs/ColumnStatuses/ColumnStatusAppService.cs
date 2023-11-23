using Abp.Application.Services.Dto;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMan.APIs.ColumnStatuses.Dto;
using ProMan.Authorization.Users;
using ProMan.Context;
using ProMan.Entities;
using ProMan.Extension;
using ProMan.Paging;
using ProMan.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.ColumnStatuses
{
    public class ColumnStatusAppService : ProManAppServiceBase
    {
        public ColumnStatusAppService(IContext context) : base(context) { }
        private readonly IContext _context;

        [HttpPost]
        public async Task<GridResult<GetColumnStatusDto>> GetAllPaging(GridParam input)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            var query = Context.GetAll<ColumnStatus>()
                .Where(s => !s.IsDeleted)
                .Select(s => new GetColumnStatusDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                });

            return await query.GetGridResult(query, input);
        }

        private async System.Threading.Tasks.Task ValColumnStatus(CreateEditColumnStatusDto input)
        {
            var isExistName = await Context.GetAll<ColumnStatus>()
                 .Where(s => s.Name == input.Name).Where(s => s.Id != input.Id).AnyAsync();

            if (isExistName)
                throw new UserFriendlyException(string
                    .Format("Column Status name {0} already existed", input.Name));
        }

        [HttpPost]
        public async Task<CreateEditColumnStatusDto> Create(CreateEditColumnStatusDto input)
        {
            await ValColumnStatus(input);
            var columnStatus = ObjectMapper.Map<ColumnStatus>(input);
            input.Id = await Context.GetRepo<ColumnStatus, long>().InsertAndGetIdAsync(columnStatus);
            CurrentUnitOfWork.SaveChanges();

            //insert projectUser
            /*
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
            */
            return input;
        }

        [HttpPost]
        public async Task<CreateEditColumnStatusDto> Edit(CreateEditColumnStatusDto input)
        {
            await ValColumnStatus(input);
            var columnStatus = await Context.GetAsync<ColumnStatus>(input.Id);
            ObjectMapper.Map<CreateEditColumnStatusDto, ColumnStatus>(input, columnStatus);
            await Context.GetRepo<ColumnStatus, long>().UpdateAsync(columnStatus);

            // projectUser
            /*
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

            foreach (var id in deleteProjectUserIds)
            {
                await Context.DeleteAsync<ProjectUser>(id);
            }

            foreach (var pUserDto in insertUsers)
            {
                var projectUser = ObjectMapper.Map<ProjectUser>(pUserDto);
                projectUser.ProjectId = input.Id;
                await Context.InsertAsync<ProjectUser>(projectUser);
            }

            foreach (var item in updateUsers)
            {
                if (item.Dto.Type != item.ProjectUser.Type)
                {
                    item.ProjectUser.Type = item.Dto.Type;

                    await Context.UpdateAsync<ProjectUser>(item.ProjectUser);
                }
            }
            */
            return input;
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task Delete(EntityDto<long> input)
        {
            var columnStatus = await Context.GetAsync<ColumnStatus>(input.Id);
            if (columnStatus != null)
            {
                await Context.GetRepo<ColumnStatus, long>().DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Column Status is not exist"));
            }
        }

        [HttpGet]
        public async Task<GetColumnStatusDto> GetOneColumnStatus(long id)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            return await Context.GetAll<ColumnStatus>()
                .Where(s => s.Id == id)
                .Where(s => !s.IsDeleted)
                .Select(s => new GetColumnStatusDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                }).FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<List<GetColumnStatusDto>> GetAllColumnStatusTicketNotPaging()
        {
            
            return await Context.GetAll<ColumnStatus>()
                .Where (s => s.Type == Constants.Enum.StatusEnum.ColumnType.Ticket)
                .Select(s => new GetColumnStatusDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                }).ToListAsync();
        }

        [HttpGet]
        public async Task<List<GetColumnStatusDto>> GetAllColumnStatusTaskNotPaging()
        {

            return await Context.GetAll<ColumnStatus>()
                .Where(s => s.Type == Constants.Enum.StatusEnum.ColumnType.Task)
                .Select(s => new GetColumnStatusDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Type = s.Type,
                }).ToListAsync();
        }
    }
}
