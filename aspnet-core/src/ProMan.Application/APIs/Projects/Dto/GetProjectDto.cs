using Abp.Domain.Entities;
using ProMan.Anotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProMan.Constants.Enum.StatusEnum;

namespace ProMan.APIs.Projects.Dto
{
    public class GetProjectDto : Entity<long>
    {
        [ApplySearch]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime UpdatedAt => LastModifierTime.HasValue ? LastModifierTime.Value : CreationTime;
        public string UpdatedName => String.IsNullOrEmpty(LastModifierUserName) ? CreatedUserName : LastModifierUserName;
        public string CreatedUserName { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModifierTime { get; set; }
    }
}
