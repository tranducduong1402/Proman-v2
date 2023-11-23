using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.Tasks.Dto
{
    [AutoMapTo(typeof(Entities.Task))]
    public class CreateEditTaskDto : EntityDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public TaskType Type { get; set; }
        //public PriorityType Priority { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
        public long ColumnStatusId { get; set; }
    }
}
