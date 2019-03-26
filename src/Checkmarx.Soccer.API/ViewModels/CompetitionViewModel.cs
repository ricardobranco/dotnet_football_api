using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.API.ViewModels
{
    public class CompetitionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CompetitionWithStandingsViewModel : CompetitionViewModel
    {
        public IEnumerable<StandingViewModel> Standings { get; set; }
    }
}
