using System.ComponentModel.DataAnnotations;

namespace GuidesApp.Services.GuidesAPI.Models
{
	public class Guide
	{
        [Key]
        public int GuideId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        [Required]
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedByDisplayName { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? LastModifiedByDisplayName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}

