using System;
using GuidesApp.Web.Models;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;

namespace GuidesApp.Web.Service
{
    public class AuthService : IAuthService
    {
        private IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RoleAssignRequestDto assignRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.AuthAPIBase + "/api/Auth/AssignRole",
                Data = assignRequestDto
            });
        }

        public async Task<ResponseDto?> CreateRoleAsync(CreateRoleRequestDto createRoleRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.AuthAPIBase + "/api/Auth/CreateRole",
                Data = createRoleRequestDto
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.AuthAPIBase + "/api/Auth/Login",
                Data = loginRequestDto
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.AuthAPIBase + "/api/Auth/Register",
                Data = registrationRequestDto
            });
        }
    }
}

