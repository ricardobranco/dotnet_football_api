using System;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string CrestUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}