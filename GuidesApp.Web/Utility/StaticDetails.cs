using System;
namespace GuidesApp.Web.Utility
{
	public class StaticDetails
	{
		public static string GuideAPIBase { get; set; }
		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}

