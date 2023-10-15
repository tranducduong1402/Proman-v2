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
    public class Project : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public string Code { get; set; }
        //public ProjectType ProjectType { get; set; }
        public string Note { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual User Customer { get; set; }
        public long CustomerId { get; set; }
    }
}
