using Abp.Domain.Entities.Auditing;
using ProMan.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;
using TaskStatus = ProMan.Constants.Enum.StatusEnum.TaskStatus;

namespace ProMan.Entities
{
    public class Task : FullAuditedEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public PriorityType Priority { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        public long? TicketId { get; set; }
    }
}
