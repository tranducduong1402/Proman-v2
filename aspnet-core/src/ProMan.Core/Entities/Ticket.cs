using Abp.Domain.Entities.Auditing;
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
    public class Ticket : FullAuditedEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public float? TimeTrackingSpent { get; set; }
        public float? TimeTrackingRemaining { get; set; }
        public float? OriginalEstimate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
        public long? ProjectId { get; set; }
    }
}
