using GuidesApp.Web.Models;
using GuidesApp.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuidesApp.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequestDto());
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            LoginResponseDto? loginResponseDto = new();
            try
            {
                ResponseDto? responseDto = await _authService.LoginAsync(loginRequestDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    string? resultJson = Convert.ToString(responseDto.Result);
                    if (!string.IsNullOrEmpty(resultJson))
                    {
                        loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(resultJson);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Failed to login. Response result contains no data.";
                        throw new Exception("Response result contains no data.");
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationRequestDto());
        }
    }
}

