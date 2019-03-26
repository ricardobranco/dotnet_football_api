using System;
using System.Collections.Generic;

namespace Checkmarx.Soccer.API.ViewModels
{
    public class StandingViewModel
    {
        public string Group { get; set; }
        public IEnumerable<TableItemViewModel> Table { get; set; }
    }
}
