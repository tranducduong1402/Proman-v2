using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProMan.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.Entities
{
    public class Task : FullAuditedEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public PriorityType Priority { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        public long? TicketId { get; set; }

        [ForeignKey(nameof(ColumnStatusId))]
        public virtual ColumnStatus ColumnStatus { get; set; }
        public long? ColumnStatusId { get; set; }
    }
}
