using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Competition>> GetCompetitions()
        {
            var request = new RestRequest("competitions", Method.GET);
            request.AddQueryParameter("plan", "TIER_ONE");
            return (await _api.Execute<Competitions>(request)).CompetitionList;
        }

        public Task<CompetitionStandings> GetCompetitionStandings<T>(T competetion)
        {
            var request = new RestRequest("competitions/{code}/standings", Method.GET);
            request.AddParameter("code", competetion, ParameterType.UrlSegment);
            request.AddQueryParameter("standingType", "TOTAL");
            return _api.Execute<CompetitionStandings>(request);
        }

    }
}
