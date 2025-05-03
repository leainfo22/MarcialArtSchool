using Microsoft.AspNetCore.Mvc;

namespace MarcialArtSchool.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
