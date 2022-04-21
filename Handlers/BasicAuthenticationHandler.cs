using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using DatabaseVmProject.DAL;
using DatabaseVmProject.Models;
using DatabaseVmProject.DTO;

namespace DatabaseVmProject.Handlers
{
    public class AppAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        // Inject the DBcontext into the handler so that we can compare te credentials
        private readonly VmEntities _context;
        private readonly ILogger<AppAuthHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // BAsic Authentication needs contructor and this is it below
        public AppAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            ILogger<AppAuthHandler> logger,
            UrlEncoder encoder,
            VmEntities context,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor
            )
            : base(options, loggerFactory, encoder, clock)
        {
            // intialize the context
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string sessionTokenValue = _httpContextAccessor.HttpContext.Session.GetString("sessionTokenValue");


            if (sessionTokenValue == Environment.GetEnvironmentVariable("BFF_PASSWORD"))
            {
                return SuccessResult("BFF_APPLICATION");
            }

            string storedCookie = _httpContextAccessor.HttpContext.Session.GetString("BESessionCookie");
            string requestCookie = _httpContextAccessor.HttpContext.Request.Cookies[".VMProject.Session"] != null ? $".VMProject.Session={_httpContextAccessor.HttpContext.Request.Cookies[".VMProject.Session"]}" : null;

            if (storedCookie == requestCookie && storedCookie != null)
            {
                User user = (from st in _context.SessionTokens
                             join at in _context.AccessTokens
                             on st.AccessTokenId equals at.AccessTokenId
                             join u in _context.Users
                             on at.UserId equals u.UserId
                             where st.SessionTokenValue == Guid.Parse(sessionTokenValue)
                             select u).FirstOrDefault();
                if (user != null)
                {
                    return SuccessResult(user.Email);
                }
            }
            else if (storedCookie == null && requestCookie == null)
            {
                return AuthenticateResult.Fail("No session established");
            }
            else if (requestCookie != null)
            {
                if (storedCookie == null)
                {
                    string[] cookieParts = requestCookie.Split('=', 2);
                    Cookie dbCookie = (from c in _context.Cookies
                                       where c.CookieName == cookieParts[0]
                                       && c.CookieValue == cookieParts[1]
                                       && c.SiteFrom == "BE"
                                       select c).FirstOrDefault();

                    if (dbCookie != null)
                    {
                        UserSession userSession = (from c in _context.Cookies
                                                   join st in _context.SessionTokens
                                                   on c.SessionTokenId equals st.SessionTokenId
                                                   join at in _context.AccessTokens
                                                   on st.AccessTokenId equals at.AccessTokenId
                                                   join u in _context.Users
                                                   on at.UserId equals u.UserId
                                                   where c == dbCookie
                                                   select new UserSession(
                                                       u,
                                                       st)).FirstOrDefault();

                        _httpContextAccessor.HttpContext.Session.SetString("BESessionCookie", $"{dbCookie.CookieName}={dbCookie.CookieValue}");
                        _httpContextAccessor.HttpContext.Session.SetString("sessionTokenValue", userSession.SessionToken.SessionTokenValue.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("userId", userSession.User.UserId.ToString());

                        return SuccessResult(userSession.User.Email);
                    }
                }
                else if (storedCookie != null)
                {
                    string[] cookiePartsStored = storedCookie.Split('=', 2);
                    string[] cookiePartsRequest = requestCookie.Split('=', 2);
                    Cookie dbCookie = (from c in _context.Cookies
                                       where c.CookieName == cookiePartsStored[0]
                                       && c.CookieValue == cookiePartsStored[1]
                                       && c.SiteFrom == "BE"
                                       select c).FirstOrDefault();
                    if (dbCookie != null)
                    {
                        dbCookie.CookieValue = cookiePartsRequest[1];
                        User user = (from st in _context.SessionTokens
                                     join at in _context.AccessTokens
                                     on st.AccessTokenId equals at.AccessTokenId
                                     join u in _context.Users
                                     on at.UserId equals u.UserId
                                     where st.SessionTokenValue == Guid.Parse(sessionTokenValue)
                                     select u).FirstOrDefault();
                        if (user != null)
                        {
                            return SuccessResult(user.Email);
                        }
                    }
                }
            }
            return AuthenticateResult.Fail("No session token");
        }

        public AuthenticateResult SuccessResult(string name)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, name) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}