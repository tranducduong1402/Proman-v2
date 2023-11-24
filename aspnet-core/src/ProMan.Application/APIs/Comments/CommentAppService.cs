using Microsoft.AspNetCore.Mvc;
using ProMan.APIs.Tickets.Dto;
using ProMan.Authorization.Users;
using ProMan.Context;
using ProMan.Entities;
using ProMan.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.Comments
{
    public class CommentAppService : ProManAppServiceBase
    {
        public CommentAppService(IContext context) : base(context) { }

        [HttpGet]
        public async Task<List<CommentTicket>> GetAll(long ticketID)
        {
            try
            {
                var rs = Context.GetAll<CommentTicket>()
                    .Where(c => c.TicketId == ticketID)
                    .OrderByDescending(c => c.LastModificationTime)
                    .Select(c => new CommentTicket()
                    {
                        Description = c.Description,
                        CreationTime = c.CreationTime,
                        UserId = c.UserId,
                        User = new User()
                        {
                            Id = c.UserId,
                            UserName = c.User.UserName,
                        },
                        TicketId = c.TicketId,
                        Id = c.Id
                    }
                    )
                    .ToList();

                return rs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        [HttpPost]
        public async Task<CommentTicket> Create(CommentTicket input)
        {
            try
            {
                input.LastModificationTime = DateTime.Now;
                var comment = ObjectMapper.Map<CommentTicket>(input);
                var repo = Context.GetRepo<CommentTicket, long>();
                var rs = await repo.InsertAsync(comment);
                CurrentUnitOfWork.SaveChanges();
                return rs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        [HttpPost]
        public async Task<CommentTicket> Update(CommentTicket input)
        {
            try
            {
                input.LastModificationTime = DateTime.Now;
                var comment = ObjectMapper.Map<CommentTicket>(input);
                var repo = Context.GetRepo<CommentTicket, long>();
                var rs = await repo.UpdateAsync(comment);
                CurrentUnitOfWork.SaveChanges();
                return rs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        [HttpDelete]
        public async Task<bool> Delete(long id)
        {
            try
            {
                var repo = Context.GetRepo<CommentTicket, long>();
                await repo.DeleteAsync(id);
                CurrentUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
