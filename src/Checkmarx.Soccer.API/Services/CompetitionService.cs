using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkmarx.Soccer.API.Interfaces;
using Checkmarx.Soccer.API.ViewModels;
using Checkmarx.Soccer.FootballData.Interfaces;
using Checkmarx.Soccer.FootballData.Models;
using Microsoft.Extensions.Logging;

namespace Checkmarx.Soccer.API.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ILogger<CompetitionService> _logger;
        private readonly IFootballDataService _footballDataService;

        public CompetitionService(ILoggerFactory loggerFactory,
        IFootballDataService footballDataService)
        {
            _logger = loggerFactory.CreateLogger<CompetitionService>();
            _footballDataService = footballDataService;
        }

        public async Task<IEnumerable<CompetitionAreaViewModel>> GetCompetitions()
        {
            _logger.LogDebug("GetCompetitions");
            var competitions = await _footballDataService.GetCompetitions();
            var areasWithCompetitions = from competition in competitions
                                        group competition by new { competition.Area.Id, competition.Area.Name } into g
                                        select new CompetitionAreaViewModel
                                        {
                                            Name = g.Key.Name,
                                            Competitions = g.Select(c => new CompetitionViewModel
                                            {
                                                Id = c.Id,
                                                Name = c.Name
                                            })
                                        };
            return areasWithCompetitions;
        }

        public async Task<CompetitionWithStandingsViewModel> GetCompetitionWithStandings(int competitionId)
        {
            _logger.LogDebug("GetCompetitionWithStandings");
            var competitionStandings = await _footballDataService.GetCompetitionStandings(competitionId);
            return new CompetitionWithStandingsViewModel
            {
                Id = competitionStandings.Competition.Id,
                Name = competitionStandings.Competition.Name,
                Standings = BuildStandings(competitionStandings.Standings)
            };
        }

        private IEnumerable<StandingViewModel> BuildStandings(IEnumerable<Standing> standings)
        {
            return from standing in standings
                   select new StandingViewModel
                   {
                       Group = standing.Group,
                       Table = BuildTable(standing.Table)
                   };
        }

        private IEnumerable<TableItemViewModel> BuildTable(IEnumerable<TableItem> table)
        {
            return from team in table
                   select new TableItemViewModel
                   {
                       Name = team.Team.Name,
                       Position = team.Position,
                       PlayedGames = team.PlayedGames,
                       Won = team.Won,
                       Draw = team.Draw,
                       Lost = team.Lost,
                       GoalsFor = team.GoalsFor,
                       GoalsAgainst = team.GoalsAgainst,
                       GoalDifference = team.GoalDifference
                   };
        }
    }
}
