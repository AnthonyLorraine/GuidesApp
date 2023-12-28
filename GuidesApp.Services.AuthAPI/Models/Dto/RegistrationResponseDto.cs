using System;
namespace GuidesApp.Services.AuthAPI.Models.Dto
{
    public class RegistrationResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
	}
}

