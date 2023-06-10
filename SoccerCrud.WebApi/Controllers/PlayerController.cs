namespace SoccerCrud.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var taskResult = await _playerService.GetAllAsync();

            if (taskResult == null)
            {
                return NotFound();
            }

            return Ok(taskResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var team = await _playerService.GetAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayerDto createPlayerDto)
        {
            var createdPlayerDto = await _playerService.CreateAsync(createPlayerDto);

            if (createdPlayerDto == null)
            {
                return NotFound();
            }

            return Ok(createdPlayerDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlayerDto updatePlayerDto)
        {
            var playerDto = await _playerService.UpdateAsync(id, updatePlayerDto);

            if (playerDto == null)
            {
                return BadRequest();
            }

            return Ok(playerDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var taskResult = await _playerService.DeleteAsync(id);

            if (!taskResult)
            {
                return BadRequest();
            }

            return Ok(taskResult);
        }
    }
}
