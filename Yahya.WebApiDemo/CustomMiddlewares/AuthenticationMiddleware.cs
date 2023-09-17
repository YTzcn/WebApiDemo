using System.Security.Claims;
using System.Text;

namespace Yahya.WebApiDemo.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string AuthHeader = httpContext.Request.Headers["Authorization"];
            if (AuthHeader == null)
            {
                httpContext.Response.StatusCode = 401;
                await _next(httpContext);
                return;
            }
            if (AuthHeader != null && AuthHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = AuthHeader.Substring(6).Trim();
                var cridentialString = "";
                try
                {
                    cridentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                var cridentials = cridentialString.Split(':');
                if (cridentials[0] == "yahya" && cridentials[1] == "12345")
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, cridentials[0]),
                        new Claim(ClaimTypes.Role,"Admin")

                    };
                    var identity = new ClaimsIdentity(claims, "basic");
                    httpContext.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                }

            }
            else
            {
                httpContext.Response.StatusCode = 401;
            }
            await _next(httpContext);
        }
    }

}
