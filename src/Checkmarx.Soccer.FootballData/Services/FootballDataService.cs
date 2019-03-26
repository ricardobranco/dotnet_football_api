using System;
using System.Threading.Tasks;
using Checkmarx.Soccer.FootballData.Interfaces;
using Checkmarx.Soccer.FootballData.Models;
using RestSharp;

namespace Checkmarx.Soccer.FootballData.Services
{
    public class FootballDataService : IFootballDataService
    {
        private readonly FootballDataApi _api;

        public FootballDataService(FootballDataApi api)
        {
            _api = api;
        }

        public Task<Competitions> GetCompetitions()
        {
            var request = new RestRequest("competitions", Method.GET);
            request.AddQueryParameter("plan", "TIER_ONE");
            return _api.Execute<Competitions>(request);

        }

        public Task<CompetitionTeams> GetTeamsOfCompetition(string competitionCode)
        {
            var request = new RestRequest("competitions/{code/teams", Method.GET);
            request.AddParameter("code", competitionCode);
            return _api.Execute<CompetitionTeams>(request);
        }


        public Task<CompetitionStandings> GetCompetitionStandings(string competitionCode)
        {
            var request = new RestRequest("competitions/{code}/standings", Method.GET);
            request.AddParameter("code", competitionCode);
            request.AddQueryParameter("standingType", "TOTAL");
            return _api.Execute<CompetitionStandings>(request);
        }
    }
}
