using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using GuidesApp.Web.Models;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;

namespace GuidesApp.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
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
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto? loginResponseDto = new();
            try
            {
                ResponseDto? responseDto = await _authService.LoginAsync(loginRequestDto);
                if (responseDto == null)
                {
                    TempData["ErrorMessage"] = $"Failed to login. Response from API is null";
                    return RedirectToAction("Login", "Auth", new LoginRequestDto());
                }

                if (!responseDto.IsSuccess)
                {
                    TempData["ErrorMessage"] = $"Failed to login. {responseDto.Message}";
                    return RedirectToAction("Login", "Auth", new LoginRequestDto());
                }

                if (responseDto.Result == null)
                {
                    TempData["ErrorMessage"] = $"Failed to login. Result is null";
                    return RedirectToAction("Login", "Auth", new LoginRequestDto());
                }

                string? resultJson = Convert.ToString(responseDto.Result);

                if (string.IsNullOrEmpty(resultJson))
                {
                    TempData["ErrorMessage"] = $"Failed to login. Response result contains no data.";
                    return RedirectToAction("Login", "Auth", new LoginRequestDto());

                }

                loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(resultJson);
                if (loginResponseDto == null)
                {
                    TempData["ErrorMessage"] = $"Failed to login. Couldn't deserialize login response";
                    return RedirectToAction("Login", "Auth", new LoginRequestDto());
                }

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                TempData["SuccessMessage"] = $"Login Successful";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to login. {ex.Message}";
                return RedirectToAction("Login", "Auth", new LoginRequestDto());
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationRequestDto());
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Login", "Auth", new LoginRequestDto());
        }

        private bool ResponseIsSuccessful(ResponseDto responseDto)
        {
            return responseDto != null && responseDto.IsSuccess && responseDto.Result != null;
        }
        private string ResponseResultToString(ResponseDto responseDto)
        {
            if (!ResponseIsSuccessful(responseDto))
            {
                string message = responseDto is null ? "API response is null" : responseDto.Message;
                return $"Failed to Register. {message}";  
            }

            return Convert.ToString(responseDto.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            if (!ModelState.IsValid) return View();

            // Register user
            ResponseDto? registerAsyncResponse = await _authService.RegisterAsync(registrationRequestDto);

            if (!ResponseIsSuccessful(registerAsyncResponse))
            {
                string message = ResponseResultToString(registerAsyncResponse);
                TempData["ErrorMessage"] = $"Failed to Register. {message}";
                return View();
            }
            
            // Assign default "User" Role
            RoleAssignRequestDto roleAssignRequestDto = new()
            {
                UserName = registrationRequestDto.UserName,
                RoleName = StaticDetails.RoleCustomer
            };
            await _authService.AssignRoleAsync(roleAssignRequestDto);

            // Log in the newly registered user
            LoginRequestDto loginRequestDto = new()
            {
                UserName = registrationRequestDto.UserName,
                Password = registrationRequestDto.Password
            };

            ResponseDto? userLoginResponseDto = await _authService.LoginAsync(loginRequestDto);
            LoginResponseDto? loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(userLoginResponseDto.Result));

            if (loginResponseDto == null || !loginResponseDto.IsSuccess || string.IsNullOrEmpty(loginResponseDto.Token))
            {
                string message = loginResponseDto is null ? $"Registration API response is null" : loginResponseDto.Message;
                TempData["ErrorMessage"] = $"User registered, failed to login as user {registrationRequestDto.UserName}, {message}";
                return View();
            }

            // Set JWT token to user cookies
            _tokenProvider.ClearToken();
            await SignInUser(loginResponseDto);
            _tokenProvider.SetToken(loginResponseDto.Token);

            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponseDto.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var claim in jwt.Claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value));
                if (claim.Type == JwtRegisteredClaimNames.Name)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, claim.Value));
                }
            };

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

