using System;
using System.Threading.Tasks;
using Checkmarx.Soccer.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkmarx.Soccer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private readonly ICompetitionViewModelService _competitionService;

        public CompetitionsController(ICompetitionViewModelService competitionService)
        {
            _competitionService = competitionService;
        }


        // GET api/competitions
        [HttpGet]
        public async Task<IActionResult> GetCompetition()
        {
            var competitions = await _competitionService.GetCompetitions();
            return Ok(competitions);
        }

        // GET api/competitions/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompetition(int id)
        {
            var competition = await _competitionService.GetCompetitionWithStandings(id);
            return Ok(competition);
        }
    }
}
