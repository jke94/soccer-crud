namespace SoccerCrud.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Task.FromResult(id);

            return Ok($"TODO! {result}");
        }
    }
}
