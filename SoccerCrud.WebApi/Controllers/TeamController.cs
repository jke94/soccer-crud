using Microsoft.AspNetCore.Mvc;

namespace SoccerCrud.WebApi.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
