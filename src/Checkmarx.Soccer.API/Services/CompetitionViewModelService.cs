using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkmarx.Soccer.API.Interfaces;
using Checkmarx.Soccer.API.ViewModels;
using Checkmarx.Soccer.Domain.Entities;
using Checkmarx.Soccer.Domain.Interfaces;
using Checkmarx.Soccer.FootballData.Interfaces;
using Microsoft.Extensions.Logging;

namespace Checkmarx.Soccer.API.Services
{
    public class CompetitionViewModelService : ICompetitionViewModelService
    {
        private readonly ILogger<CompetitionViewModelService> _logger;
        private readonly IFootballDataService _footballDataService;
        private readonly IAsyncRepository<CompetitionArea> _areaRepository;
        private readonly IAsyncRepository<Competition> _competitionRepository;
        private readonly IAsyncRepository<Standing> _standingRepository;
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IAsyncRepository<TableItem> _tableItemRepository;

        public CompetitionViewModelService(
            ILoggerFactory loggerFactory,
            IFootballDataService footballDataService,
            IAsyncRepository<CompetitionArea> areaRepository,
            IAsyncRepository<Competition> competitionRepository,
            IAsyncRepository<Team> teamRepository,
            IAsyncRepository<Standing> standingRepository,
            IAsyncRepository<TableItem> tableItemRepository)
        {
            _logger = loggerFactory.CreateLogger<CompetitionViewModelService>();
            _footballDataService = footballDataService;
            _areaRepository = areaRepository;
            _competitionRepository = competitionRepository;
            _standingRepository = standingRepository;
            _teamRepository = teamRepository;
            _tableItemRepository = tableItemRepository;
        }

        public async Task<IEnumerable<CompetitionAreaViewModel>> GetCompetitions()
        {
            _logger.LogDebug("GetCompetitions");
            var fetchCompetitions = await _footballDataService.GetCompetitions();

            foreach (var fetchCompetition in fetchCompetitions)
            {
                CompetitionArea area = (await _areaRepository.ListAsync(a => a.Name.Equals(fetchCompetition.Area.Name, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
                if (area == null)
                {
                    area = new CompetitionArea
                    {
                        Name = fetchCompetition.Area.Name
                    };
                    await _areaRepository.AddAsync(area);
                }

                Competition competition = (await _competitionRepository.ListAsync(c => c.Code.Equals(fetchCompetition.Code, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
                if (competition == null)
                {
                    competition = new Competition
                    {
                        Code = fetchCompetition.Code,
                        Name = fetchCompetition.Name,
                        AreaId = area.Id
                    };
                    await _competitionRepository.AddAsync(competition);
                }
            }
            return from competition in await _competitionRepository.ListAllAsync()
                   group competition by new { competition.Area.Id, competition.Area.Name } into gArea
                   select new CompetitionAreaViewModel
                   {
                       Name = gArea.Key.Name,
                       Competitions = gArea.Select(c => new CompetitionViewModel
                       {
                           Id = c.Id,
                           Name = c.Name
                       })
                   };
        }

        public async Task<CompetitionWithStandingsViewModel> GetCompetitionWithStandings(int competitionId)
        {
            _logger.LogDebug("GetCompetitionWithStandings");
            Competition competition = await _competitionRepository.GetByIdAsync(competitionId);
            var competitionStandings = await _footballDataService.GetCompetitionStandings(competition.Code);
            if (competitionStandings.Competition.LastUpdated > competition.LastUpdated)
            {
                foreach (var fetchStanding in competitionStandings.Standings)
                {
                    Standing standing = (await _standingRepository.ListAsync(s => s.CompetitionId == competition.Id && s.Group.Equals(fetchStanding.Group, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
                    if (standing == null)
                    {
                        standing = new Standing
                        {
                            Group = fetchStanding.Group,
                            CompetitionId = competition.Id
                        };
                        await _standingRepository.AddAsync(standing);
                    }
                    foreach (var fetchTableTeam in fetchStanding.Table)
                    {
                        Team team = (await _teamRepository.ListAsync(t => t.Name.Equals(fetchTableTeam.Team.Name))).FirstOrDefault();
                        if (team == null)
                        {
                            team = new Team
                            {
                                Name = fetchTableTeam.Team.Name
                            };
                            await _teamRepository.AddAsync(team);
                        }
                        TableItem tableItem = (await _tableItemRepository.ListAsync(t => t.TeamId == team.Id && t.StandingId == standing.Id)).FirstOrDefault();
                        if (tableItem == null)
                        {
                            tableItem = new TableItem
                            {
                                TeamId = team.Id,
                                StandingId = standing.Id,
                                Position = fetchTableTeam.Position,
                                PlayedGames = fetchTableTeam.PlayedGames,
                                Won = fetchTableTeam.Won,
                                Draw = fetchTableTeam.Draw,
                                Lost = fetchTableTeam.Lost,
                                GoalsFor = fetchTableTeam.GoalsFor,
                                GoalsAgainst = fetchTableTeam.GoalsAgainst,
                                GoalDifference = fetchTableTeam.GoalDifference
                            };
                            await _tableItemRepository.AddAsync(tableItem);
                        }
                        else
                        {
                            tableItem.Position = fetchTableTeam.Position;
                            tableItem.PlayedGames = fetchTableTeam.PlayedGames;
                            tableItem.Won = fetchTableTeam.Won;
                            tableItem.Draw = fetchTableTeam.Draw;
                            tableItem.Lost = fetchTableTeam.Lost;
                            tableItem.GoalsFor = fetchTableTeam.GoalsFor;
                            tableItem.GoalsAgainst = fetchTableTeam.GoalsAgainst;
                            tableItem.GoalDifference = fetchTableTeam.GoalDifference;
                            await _tableItemRepository.UpdateAsync(tableItem);
                        }
                    }
                }
                competition.LastUpdated = competitionStandings.Competition.LastUpdated;
                await _competitionRepository.UpdateAsync(competition);
            }

            return new CompetitionWithStandingsViewModel
            {
                Id = competition.Id,
                Name = competition.Name,
                Standings = BuildStandings(competition.Standings.OrderBy(s => s.Group))
            };
        }

        private IEnumerable<StandingViewModel> BuildStandings(IEnumerable<Standing> standings)
        {
            return from standing in standings
                   select new StandingViewModel
                   {
                       Group = standing.Group,
                       Table = BuildTable(standing.Table.OrderBy(t => t.Position))
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
