using Microsoft.AspNetCore.Mvc;

namespace MarcialArtSchool.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
