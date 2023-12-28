using System;
using GuidesApp.Services.AuthAPI.Data;
using GuidesApp.Services.AuthAPI.Models;
using GuidesApp.Services.AuthAPI.Models.Dto;
using GuidesApp.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace GuidesApp.Services.AuthAPI.Services
{
	public class AuthService : IAuthService
	{
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginResponseDto = new();
            try
            {
                ApplicationUser? user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
                if (user == null)
                {
                    throw new Exception("No user found");
                }
                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (!isValid)
                {
                    throw new Exception("Password is incorrect");
                }

                UserDto userDto = new()
                {
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Id = user.Id
                };

                loginResponseDto.User = userDto;
                loginResponseDto.Token = _jwtTokenGenerator.GenerateToken(user);

            } catch (Exception ex) {
                loginResponseDto.Message = ex.Message;
                loginResponseDto.IsSuccess = false;
            }



            return loginResponseDto;
        }

        public async Task<RegistrationResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            RegistrationResponseDto registrationResponseDto = new();
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.UserName,
                DisplayName = registrationRequestDto.DisplayName
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDto.UserName);

                    UserDto userDto = new()
                    {
                        Id = userToReturn.Id,
                        DisplayName = userToReturn.DisplayName,
                        UserName = userToReturn.UserName
                    };
                    registrationResponseDto.Result = userDto;
                    return registrationResponseDto;
                } else {
                    throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            } catch (Exception ex)
            {
                registrationResponseDto.IsSuccess = false;
                registrationResponseDto.Message = $"Failed to register user: {ex.Message}";
            }
            return registrationResponseDto;
        }
    }
}

