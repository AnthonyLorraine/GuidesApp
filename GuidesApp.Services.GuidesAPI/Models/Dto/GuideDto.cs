using System;
namespace GuidesApp.Services.GuidesAPI.Models.Dto
{
	public class GuideDto
	{
        public int GuideId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
    }
}

