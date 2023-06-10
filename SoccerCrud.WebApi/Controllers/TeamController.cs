namespace SoccerCrud.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var taskResult = await _teamService.GetAllAsync();

            if (taskResult == null)
            {
                return NotFound();
            }

            return Ok(taskResult);
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeamDto updateTeamDto)
        {
            var teamDto = await _teamService.UpdateAsync(id, updateTeamDto);

            if (teamDto == null)
            {
                return BadRequest();
            }

            return Ok(teamDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var taskResult = await _teamService.DeleteAsync(id);

            if (!taskResult)
            {
                return BadRequest();
            }

            return Ok(taskResult);
        }
    }
}
