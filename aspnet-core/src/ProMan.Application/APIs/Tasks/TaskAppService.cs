using Abp.Application.Services.Dto;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMan.APIs.Tasks.Dto;
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

namespace ProMan.APIs.Tasks
{
    public class TaskAppService : ProManAppServiceBase
    {
        public TaskAppService(IContext context) : base(context) { }

        [HttpPost]
        public async Task<GridResult<GetTaskDto>> GetAllPaging(GridParam input)
        {
            var dicUsers = Context.GetAll<User>()
                .Select(s => new
                {
                    s.Id,
                    s.FullName
                })
                .ToDictionary(s => s.Id, s => s.FullName);

            var query = Context.GetAll<Entities.Task>()
                .Where(s => !s.IsDeleted)
                .Select(s => new GetTaskDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    //Type = s.Type,
                    //Priority = s.Priority,
                    UserId = s.User.Id,
                    TicketId = s.Ticket.Id,
                    ColumnStatusId = s.ColumnStatus.Id,
                    userName = s.User.UserName,
                    ticketTitle = s.Ticket.Title,
                    columnStatusName = s.ColumnStatus.Name,
                });

            return await query.GetGridResult(query, input);
        }
        private async System.Threading.Tasks.Task ValTicket(CreateEditTaskDto input)
        {
            var isExistName = await Context.GetAll<Entities.Task>()
                 .Where(s => s.Title == input.Title).Where(s => s.Id != input.Id).AnyAsync();

            if (isExistName)
                throw new UserFriendlyException(string
                    .Format("Task title {0} already existed", input.Title));
        }

        [HttpPost]
        public async Task<CreateEditTaskDto> Create(CreateEditTaskDto input)
        {
            await ValTicket(input);
            var taskEntity = ObjectMapper.Map<Entities.Task>(input);
            input.Id = await Context.GetRepo<Entities.Task, long>().InsertAndGetIdAsync(taskEntity);
            CurrentUnitOfWork.SaveChanges();

            return input;
        }

        [HttpPost]
        public async Task<CreateEditTaskDto> Edit(CreateEditTaskDto input)
        {
            await ValTicket(input);
            var taskEntity = await Context.GetAsync<Entities.Task>(input.Id);
            ObjectMapper.Map<CreateEditTaskDto, Entities.Task>(input, taskEntity);
            await Context.GetRepo<Entities.Task, long>().UpdateAsync(taskEntity);

            return input;
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task Delete(EntityDto<long> input)
        {
            var taskEntity = await Context.GetAsync<Entities.Task>(input.Id);
            if (taskEntity != null)
            {
                await Context.GetRepo<Entities.Task, long>().DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException(string.Format("Task is not exist !"));
            }
        }

        [HttpGet]
        public async Task<GetTaskDto> GetOneTask(long id)
        {
            var dicUsers = Context.GetAll<User>()
                                    .Select(s => new
                                    {
                                        s.Id,
                                        s.FullName,
                                    }).ToDictionary(s => s.Id, s => s.FullName);

            return await Context.GetAll<Entities.Task>()
                .Where(s => s.Id == id)
                .Where(s => !s.IsDeleted)
                .Select(s => new GetTaskDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    //Type = s.Type,
                    //Priority = s.Priority,
                    UserId = s.User.Id,
                    TicketId = s.Ticket.Id,
                    ColumnStatusId = s.ColumnStatus.Id,
                    userName = s.User.UserName,
                    ticketTitle = s.Ticket.Title,
                    columnStatusName = s.ColumnStatus.Name,
                }).FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<List<GetTaskDto>> GetAllTaskNotPaging()
        {
            return await Context.GetAll<Entities.Task>()
                .Select(s => new GetTaskDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    //Type = s.Type,
                    //Priority = s.Priority,
                    UserId = s.User.Id,
                    TicketId = s.Ticket.Id,
                    ColumnStatusId = s.ColumnStatus.Id,
                    userName = s.User.UserName,
                    ticketTitle = s.Ticket.Title,
                    columnStatusName = s.ColumnStatus.Name,
                }).ToListAsync();
        }
    }
}
