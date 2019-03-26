using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class Competitions
    {
        [DeserializeAs(Name = "competitions")]
        public IEnumerable<Competition> CompetitionList { get; set; }
    }
}
