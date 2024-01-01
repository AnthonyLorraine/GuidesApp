using System;
namespace GuidesApp.Web.Models
{
	public class GuideDto
	{
        public int GuideId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }

    public class CreateGuideDto
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class UpdateGuideDto
    {
        public int GuideId { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}

