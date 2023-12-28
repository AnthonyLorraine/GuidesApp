using System;
namespace GuidesApp.Web.Models
{
	public class LoginResponseDto
	{
		public UserDto User { get; set; }
		public string Token { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}

