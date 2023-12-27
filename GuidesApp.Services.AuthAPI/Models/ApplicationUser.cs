using System;
using Microsoft.AspNetCore.Identity;

namespace GuidesApp.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? DisplayName { get; set; }
	}
}

