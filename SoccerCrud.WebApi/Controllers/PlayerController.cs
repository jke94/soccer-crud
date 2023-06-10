using Microsoft.AspNetCore.Mvc;

namespace SoccerCrud.WebApi.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
