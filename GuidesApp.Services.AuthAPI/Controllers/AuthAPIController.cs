using System.Net;
using GuidesApp.Services.AuthAPI.Models.Dto;
using GuidesApp.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuidesApp.Services.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IAuthService _authService;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            RegistrationResponseDto registrationResponseDto = await _authService.Register(registrationRequestDto);
            if (registrationResponseDto.IsSuccess)
            {
                _response.Result = registrationResponseDto.Result;
                statusCode = StatusCodes.Status201Created;
            } else
            {
                _response.Message = registrationResponseDto.Message;
                _response.IsSuccess = false;
            }
            
            return StatusCode(statusCode, _response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            int statusCode = StatusCodes.Status401Unauthorized;
            LoginResponseDto loginResponseDto = await _authService.Login(loginRequestDto);
            if (loginResponseDto.IsSuccess)
            {
                _response.Result = loginResponseDto;
                statusCode = StatusCodes.Status200OK;
            } else
            {
                _response.Message = loginResponseDto.Message;
                _response.IsSuccess = false;
            }

            return StatusCode(statusCode, _response);
        }
    }
}
