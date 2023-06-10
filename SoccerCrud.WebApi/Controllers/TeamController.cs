using Microsoft.AspNetCore.Mvc;
using SoccerCrud.WebApi.Dto;
using SoccerCrud.WebApi.Services;

namespace SoccerCrud.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : Controller
    {
        private ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var team = await _teamService.GetAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto createTeamDto)
        {
            var createdTeamDto = await _teamService.CreateAsync(createTeamDto);

            if (createdTeamDto == null)
            {
                return NotFound();
            }

            return Ok(createdTeamDto);
        }
    }
}
