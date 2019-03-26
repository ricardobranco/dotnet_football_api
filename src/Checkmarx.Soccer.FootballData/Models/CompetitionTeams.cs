using System;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class CompetitionTeams
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}
