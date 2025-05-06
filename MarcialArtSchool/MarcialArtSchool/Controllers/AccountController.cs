using Account.DTO.LoginDTO;
using MarcialArtSchool.Core.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MarcialArtSchool.Core.RepositoryContracts;

namespace MarcialArtSchool.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            
            return View(registerDTO);
            
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
            var result = await _accountRepository.GetPupilByUserPass(loginDTO);

            if (result)
            {
                return RedirectToAction(nameof(HomeController.Index), "home");
            }

            ModelState.AddModelError("Login", "Inalid email or password");
            return View(loginDTO);

        }

    }
}
