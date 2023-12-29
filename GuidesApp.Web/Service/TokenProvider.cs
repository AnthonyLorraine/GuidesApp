using System;
using GuidesApp.Web.Service.IService;
using GuidesApp.Web.Utility;

namespace GuidesApp.Web.Service
{
	public class TokenProvider : ITokenProvider
	{
        private readonly IHttpContextAccessor _contextAccessor;

		public TokenProvider(IHttpContextAccessor contextAccessor)
		{
            _contextAccessor = contextAccessor;
		}

        void ITokenProvider.ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetails.TokenCookie);
        }

        string? ITokenProvider.GetToken()
        {
            string? token = null;
    

            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);

            return hasToken is true ? token: null;
        }

        void ITokenProvider.SetToken(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict 
            };
            _contextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token, cookieOptions);
        }
    }
}

