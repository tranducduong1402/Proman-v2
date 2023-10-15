using Abp.Domain.Entities.Auditing;
using ProMan.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.Entities
{
    public class CommentTicket : FullAuditedEntity<long>
    {
        public string Description { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        public long? TicketId { get; set; }
    }
}
