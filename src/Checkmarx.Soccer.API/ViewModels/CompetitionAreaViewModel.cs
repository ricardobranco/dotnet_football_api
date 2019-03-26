using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.API.ViewModels
{
    public class CompetitionAreaViewModel
    {
        public string Name { get; set; }
        public IEnumerable<CompetitionViewModel> Competitions { get; set; }
    }
}
