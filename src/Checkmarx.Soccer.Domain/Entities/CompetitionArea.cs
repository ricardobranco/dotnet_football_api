using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.Domain.Entities
{
    public class CompetitionArea : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
