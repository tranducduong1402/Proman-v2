using Abp.Application.Services.Dto;
using ProMan.Anotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.ColumnStatuses.Dto
{
    public class GetColumnStatusDto : EntityDto<long>
    {
        [ApplySearch]
        public string Name { get; set; }
        public ColumnType Type { get; set; }
    }
}
