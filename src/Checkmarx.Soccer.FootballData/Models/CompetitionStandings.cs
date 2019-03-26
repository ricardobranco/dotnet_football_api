using System.Collections.Generic;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class CompetitionStandings
    {
        public Competition Competition { get; set; }
        public IEnumerable<Standing> Standings { get; set; }
    }
}
