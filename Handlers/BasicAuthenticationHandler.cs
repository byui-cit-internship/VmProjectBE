using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using DatabaseVmProject.DAL;
using DatabaseVmProject.Models;

namespace DatabaseVmProject.Handlers
{
    public class AppAuthHandler: AuthenticationHandler<AuthenticationSchemeOptions>
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
            string sessionId = _httpContextAccessor.HttpContext.Session.GetString("tokenId");


            if (sessionId == Environment.GetEnvironmentVariable("BFF_PASSWORD"))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "BFF_APPLICATION") };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else if (sessionId != null)
            {
                User user = (from st in _context.SessionTokens
                             join at in _context.AccessTokens
                             on st.AccessTokenId equals at.AccessTokenId
                             join u in _context.Users
                             on at.UserId equals u.UserId
                             where st.SessionTokenValue == Guid.Parse(sessionId)
                             select u).FirstOrDefault();
                if (user != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Invalid authorization token");
                }
            }
            else
            {
                return AuthenticateResult.Fail("No session token");
            }
        }
    }
}