using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.Domain.Entities
{
    public class Standing : BaseEntity
    {
        public string Group { get; set; }
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual ICollection<TableItem> Table { get; set; }
    }

}
