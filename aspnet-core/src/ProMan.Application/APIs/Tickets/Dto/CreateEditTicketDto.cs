using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProMan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.Tickets.Dto
{
    [AutoMapTo(typeof(Ticket))]
    public class CreateEditTicketDto : EntityDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float? TimeTrackingSpent { get; set; }
        public float? TimeTrackingRemaining { get; set; }
        public float? OriginalEstimate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long ColumnStatusId { get; set; }
        public long ProjectId { get; set; }
    }
}
