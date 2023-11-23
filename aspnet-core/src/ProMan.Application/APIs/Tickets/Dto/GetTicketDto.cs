using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using ProMan.Anotations;
using ProMan.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.Tickets.Dto
{
    public class GetTicketDto : EntityDto<long>
    {
        [ApplySearch]
        public string Title { get; set; }
        public string Description { get; set; }
        public float? TimeTrackingSpent { get; set; }
        public float? TimeTrackingRemaining { get; set; }
        public float? OriginalEstimate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? ProjectId { get; set; }
        public long? ColumnStatusId { get; set; }
        public string projectName { get; set; }
        public string columnStatusName { get; set; }
        public DateTime? LastModifierTime { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatedUserName { get; set; }
        public string LastModifierUserName { get; set; }
    }
}
