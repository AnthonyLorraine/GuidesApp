using System;
namespace GuidesApp.Web.Utility
{
	public class StaticDetails
	{
		public static string? GuideAPIBase { get; set; }
		public static string? AuthAPIBase { get; set; }
		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}

