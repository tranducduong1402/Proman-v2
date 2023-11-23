using Abp.Application.Services.Dto;
using ProMan.Anotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.Tasks.Dto
{
    public class GetTaskDto : EntityDto<long>
    {
        [ApplySearch]
        public string Title { get; set; }
        public string Description { get; set; }
        //public TaskType Type { get; set; }
        //public PriorityType Priority { get; set; }
        public long? UserId { get; set; }
        public long? TicketId { get; set; }
        public long? ColumnStatusId { get; set; }
        public string userName { get; set; }
        public string ticketTitle { get; set; }
        public string columnStatusName { get; set; }

    }
}
