using System;
using System.ComponentModel.DataAnnotations;
using GuidesApp.Web.Utility;

namespace GuidesApp.Web.Models
{
	public class RegistrationRequestDto
	{
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string PasswordConfirmation { get; set; }
	}
}

