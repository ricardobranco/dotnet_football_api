using System;
namespace Checkmarx.Soccer.FootballData.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public Area Area { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string EmblemUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
