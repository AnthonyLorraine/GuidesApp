using System;
using System.ComponentModel.DataAnnotations;

namespace GuidesApp.Services.GuidesAPI.Models
{
	public class Guide
	{
        [Key]
        public int GuideId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        [Required]
        public string Content { get; set; }
    }
}

