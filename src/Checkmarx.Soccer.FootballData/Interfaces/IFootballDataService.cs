using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkmarx.Soccer.FootballData.Models;

namespace Checkmarx.Soccer.FootballData.Interfaces
{
    public interface IFootballDataService
    {
        Task<IEnumerable<Competition>> GetCompetitions();
        Task<CompetitionStandings> GetCompetitionStandings<T>(T competetionCode);
    }
}
