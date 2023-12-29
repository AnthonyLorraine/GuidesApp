using System;
namespace GuidesApp.Services.AuthAPI.Models.Dto
{
	public class RegistrationRequestDto
	{
		public string UserName { get; set; }
		public string DisplayName { get; set; }
		public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}

