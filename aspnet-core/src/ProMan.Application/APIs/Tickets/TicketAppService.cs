using Abp.Application.Services.Dto;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMan.APIs.ColumnStatuses.Dto;
using ProMan.APIs.Tickets.Dto;
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

namespace ProMan.APIs.Tickets
{
    public class TicketAppService : ProManAppServiceBase
    {
        public TicketAppService(IContext context) : base(context) { }

        [HttpPost]
        public async Task<GridResult<GetTicketDto>> GetAllPaging(GridParam input)
        {
            var dicUsers = Context.GetAll<User>()
                .Select(s => new
                {
                    s.Id,
                    s.FullName
                })
                .ToDictionary(s => s.Id, s => s.FullName);

            var query = Context.GetAll<Ticket>()
                .Where(s => !s.IsDeleted)
                .Select(s => new GetTicketDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    ColumnStatusId = s.ColumnStatus.Id,
                    ProjectId = s.Project.Id,
                    columnStatusName = s.ColumnStatus.Name,
                    projectName = s.Project.Name,
                    OriginalEstimate = s.OriginalEstimate,
                    TimeTrackingSpent = s.TimeTrackingSpent,
                    TimeTrackingRemaining = s.TimeTrackingRemaining,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    LastModifierTime = s.LastModificationTime,
                    CreationTime = s.CreationTime,
                    CreatedUserName = (s.CreatorUserId.HasValue && dicUsers.ContainsKey(s.CreatorUserId.Value)) ? dicUsers[s.CreatorUserId.Value] : "",
                    LastModifierUserName = (s.LastModifierUserId.HasValue && dicUsers.ContainsKey(s.LastModifierUserId.Value)) ? dicUsers[s.LastModifierUserId.Value] : "",
                });

            return await query.GetGridResult(query, input);
        }
        private async System.Threading.Tasks.Task ValTicket(CreateEditTicketDto input)
        {
            var isExistName = await Context.GetAll<Ticket>()
                 .Where(s => s.Title == input.Title).Where(s => s.Id != input.Id).AnyAsync();

            if (isExistName)
                throw new UserFriendlyException(string
                    .Format("Ticket title {0} already existed", input.Title));
        }

        [HttpPost]
        public async Task<CreateEditTicketDto> Create(CreateEditTicketDto input)
        {
            await ValTicket(input);
            var ticket = ObjectMapper.Map<Ticket>(input);
            input.Id = await Context.GetRepo<Ticket, long>().InsertAndGetIdAsync(ticket);
            CurrentUnitOfWork.SaveChanges();

            return input;
        }

        [HttpPost]
        public async Task<CreateEditTicketDto> Edit(CreateEditTicketDto input)
        {
            await ValTicket(input);
            var ticket = await Context.GetAsync<Ticket>(input.Id);
            ObjectMapper.Map<CreateEditTicketDto, Ticket>(input, ticket);
            await Context.GetRepo<Ticket, long>().UpdateAsync(ticket);

            // projectUser
            /*var currentProjectUsers = await Context.GetAll<ProjectUser>()
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
            }*/
            return input;
        }

        /*[HttpPost]
        public async System.Threading.Tasks.Task Deactive(EntityDto<long> input)
        {
            var ticket = await Context.GetAsync<Ticket>(input.Id);
            if (ticket != null)
            {
                ticket.Status = TicketStatus.Deactive;
                await Context.GetRepo<Ticket, long>().UpdateAsync(ticket);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Ticket is not exist"));
            }
        }*/

        [HttpDelete]
        public async System.Threading.Tasks.Task Delete(EntityDto<long> input)
        {
            var ticket = await Context.GetAsync<Ticket>(input.Id);
            if (ticket != null)
            {
                await Context.GetRepo<Ticket, long>().DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Ticket is not exist !"));
            }
        }

        [HttpGet]
        public async Task<GetTicketDto> GetOneTicket(long id)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            return await Context.GetAll<Ticket>()
                .Where(s => s.Id == id)
                .Where(s => !s.IsDeleted)
                .Select(s => new GetTicketDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    TimeTrackingSpent = s.TimeTrackingSpent,
                    TimeTrackingRemaining = s.TimeTrackingRemaining,
                    OriginalEstimate = s.OriginalEstimate,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    ProjectId = s.Project.Id,
                    ColumnStatusId = s.ColumnStatus.Id,
                    projectName = s.Project.Name,
                    columnStatusName = s.ColumnStatus.Name,
                }).FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<List<GetTicketDto>> GetAllTicketNotPaging()
        {
            return await Context.GetAll<Ticket>()
                .Select(s => new GetTicketDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    TimeTrackingSpent = s.TimeTrackingSpent,
                    TimeTrackingRemaining = s.TimeTrackingRemaining,
                    OriginalEstimate = s.OriginalEstimate,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    ProjectId = s.Project.Id,
                    ColumnStatusId = s.ColumnStatus.Id,
                    projectName = s.Project.Name,
                    columnStatusName = s.ColumnStatus.Name,
                }).ToListAsync();
        }
    }
}
