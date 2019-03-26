using System;
using System.Threading.Tasks;
using Checkmarx.Soccer.FootballData.Models;

namespace Checkmarx.Soccer.FootballData.Interfaces
{
    public interface IFootballDataService
    {
        Task<Competitions> GetCompetitions();
        Task<CompetitionTeams> GetTeamsOfCompetition(string competetionCode);
        Task<CompetitionStandings> GetCompetitionStandings(string competetionCode);
    }
}
