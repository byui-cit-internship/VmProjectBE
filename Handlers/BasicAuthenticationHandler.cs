using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using VmProjectBE.DAL;
using VmProjectBE.Models;
using VmProjectBE.DTO;

namespace VmProjectBE.Handlers
{
    public class AppAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        // Inject the DBcontext into the handler so that we can compare te credentials
        private readonly VmEntities _context;
        private readonly ILogger<AppAuthHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        // BAsic Authentication needs contructor and this is it below
        public AppAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            ILogger<AppAuthHandler> logger,
            UrlEncoder encoder,
            VmEntities context,
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration
            )
            : base(options, loggerFactory, encoder, clock)
        {
            // intialize the context
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {


            string vimaCookie = _httpContextAccessor.HttpContext.Request.Cookies["vima-cookie"] ?? null;

            if (vimaCookie != null)
            {
                if (vimaCookie == _configuration.GetConnectionString("BFF_PASSWORD"))
                {
                    return SuccessResult("BFF application");
                } else
                {
                    User user = (from st in _context.SessionTokens
                                 join at in _context.AccessTokens
                                 on st.AccessTokenId equals at.AccessTokenId
                                 join u in _context.Users
                                 on at.UserId equals u.UserId
                                 where st.SessionTokenValue == Guid.Parse(vimaCookie)
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        return SuccessResult(user.Email);
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