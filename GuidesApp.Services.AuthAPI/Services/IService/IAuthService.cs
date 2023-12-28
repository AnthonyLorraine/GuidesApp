using System;
using GuidesApp.Services.AuthAPI.Models.Dto;

namespace GuidesApp.Services.AuthAPI.Services.IService
{
	public interface IAuthService
	{
		Task<RegistrationResponseDto> Register(RegistrationRequestDto registrationRequestDto);
		Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
		Task<ResponseDto> AssignRole(string userName, string roleName);
		Task<ResponseDto> CreateRole(string roleName);
	}
}

