using System;
using System.ComponentModel.DataAnnotations;

namespace GuidesApp.Services.GuidesAPI.Models.Dto
{
    public class GuideDto
    {
        public int GuideId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedByDisplayName { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? LastModifiedByDisplayName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }

    public class CreateGuideDto
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Subtitle { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        public string? CreatedByDisplayName { get; set; }
    }

    public class UpdateGuideDto
    {
        public int GuideId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? LastModifiedByDisplayName { get; set; }
    }
}