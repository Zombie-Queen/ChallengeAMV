using Microsoft.AspNetCore.Http;

namespace AMV.TourReservation.Web.Service
{
    public class TokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string JWTCookieName = "JWTCookie";

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(JWTCookieName);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(JWTCookieName, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(JWTCookieName, token);
        }
    }
}
