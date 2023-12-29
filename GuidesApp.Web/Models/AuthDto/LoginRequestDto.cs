using System;
using System.ComponentModel.DataAnnotations;

namespace GuidesApp.Web.Models
{
	public class LoginRequestDto
	{
		[Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
	}
}

