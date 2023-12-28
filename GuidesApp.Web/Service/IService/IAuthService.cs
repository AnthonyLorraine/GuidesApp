using System;
using GuidesApp.Web.Models;

namespace GuidesApp.Web.Service.IService
{
	public interface IAuthService
	{
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RoleAssignRequestDto assignRequestDto);
        Task<ResponseDto?> CreateRoleAsync(CreateRoleRequestDto createRoleRequestDto);
    }
}

