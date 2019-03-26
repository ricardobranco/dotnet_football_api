using System;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class CompetitionTeams
    {
        public IList<Team> Teams { get; set; }
    }
}
