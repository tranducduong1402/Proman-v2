using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProMan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.ColumnStatuses.Dto
{
    [AutoMapTo(typeof(ColumnStatus))]
    public class CreateEditColumnStatusDto : EntityDto<long>
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
    }
}
