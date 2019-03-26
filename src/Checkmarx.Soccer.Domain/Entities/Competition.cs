using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.Domain.Entities
{
    public class Competition : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int AreaId { get; set; }
        public virtual CompetitionArea Area { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual ICollection<Standing> Standings { get; set; }
    }
}
