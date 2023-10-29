using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.Entities
{
    public class ColumnStatus : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }
        public long? TicketId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }
        public long? TaskId { get; set; }
    }
}
