using System;
using GuidesApp.Services.AuthAPI.Models;

namespace GuidesApp.Services.AuthAPI.Services.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser user);
	}
}

