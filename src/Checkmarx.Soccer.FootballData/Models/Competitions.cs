using System.Collections.Generic;
using RestSharp.Serializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class Competitions
    {
        [SerializeAs(Name = "competitions")]
        public IList<Competition> CompetitionList { get; set; }
    }
}
