using System;
namespace GuidesApp.Web.Utility
{
	public class StaticDetails
	{
		public static string? GuideAPIBase { get; set; }
		public static string? AuthAPIBase { get; set; }
		public static string RoleAdmin { get; set; } = "Administrator";
		public static string RoleCustomer { get; set; } = "User";

        public const string TokenCookie = "JwtToken";
		public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}

