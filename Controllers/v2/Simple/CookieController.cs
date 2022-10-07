using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;
using System.Linq;
using System.Reflection;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CookieController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VmEntities _context;
        private readonly ILogger<CookieController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieController(
            IConfiguration configuration,
            VmEntities context,
            ILogger<CookieController> logger,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _auth = new(_context, _logger);
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        /****************************************

        ****************************************/
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult> GetCookie(
            [FromQuery] int? cookieId,
            [FromQuery] int? sessionTokenId,
            [FromQuery] string cookieName,
            [FromQuery] string cookieValue,
            [FromQuery] string siteFrom,
            [FromQuery] string sessionTokenValue)
        {
            // Gets email from session
            //bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            //int accessUserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            //User professor = _auth.getAdmin(accessUserId);
            // Returns a professor user or null if email is not associated with a professor

            //if (isSystem || professor != null)
            //{
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("cookieId", cookieId),
                    ("sessionTokenId", sessionTokenId),
                    ("cookieName", cookieName),
                    ("cookieValue", cookieValue),
                    ("siteFrom", siteFrom),
                    ("sessionTokenValue", sessionTokenValue));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from c in _context.Cookies
                             select c).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "cookieId":
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieId == cookieId
                                     select c).FirstOrDefault());
                            case "sessionTokenId":
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.SessionTokenId == sessionTokenId
                                     select c).ToList());
                            case "cookieName":
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieName == cookieName
                                     select c).ToList());
                            case "cookieValue":
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieValue == cookieValue
                                     select c).ToList());
                            case "siteFrom":
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.SiteFrom == siteFrom
                                     select c).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                                validParameters.Contains("sessionTokenId") &&
                                validParameters.Contains("cookieName"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.SessionTokenId == sessionTokenId
                                     && c.CookieName == cookieName
                                     select c).FirstOrDefault());
                            case bool ifTrue when
                                validParameters.Contains("sessionTokenId") &&
                                validParameters.Contains("cookieValue"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.SessionTokenId == sessionTokenId
                                     && c.CookieValue == cookieValue
                                     select c).ToList());
                            case bool ifTrue when
                                validParameters.Contains("sessionTokenId") &&
                                validParameters.Contains("siteFrom"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.SessionTokenId == sessionTokenId
                                     && c.SiteFrom == siteFrom
                                     select c).ToList());
                            case bool ifTrue when
                                validParameters.Contains("cookieName") &&
                                validParameters.Contains("cookieValue"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieName == cookieName
                                         && c.CookieValue == cookieValue
                                     select c).ToList());
                            case bool ifTrue when
                                validParameters.Contains("cookieName") &&
                                validParameters.Contains("siteFrom"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieName == cookieName
                                         && c.SiteFrom == siteFrom
                                     select c).ToList());
                            case bool ifTrue when
                                validParameters.Contains("cookieValue") &&
                                validParameters.Contains("siteFrom"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieValue == cookieValue
                                         && c.SiteFrom == siteFrom
                                     select c).FirstOrDefault());
                        case bool ifTrue when
                                validParameters.Contains("siteFrom") &&
                                validParameters.Contains("sessionTokenValue"):
                            return Ok(
                                (from c in _context.Cookies
                                 join st in _context.SessionTokens
                                 on c.SessionTokenId equals st.SessionTokenId
                                 where c.SiteFrom == siteFrom
                                     && st.SessionTokenValue == Guid.Parse(sessionTokenValue)
                                 select c).FirstOrDefault());
                        default:
                                return BadRequest("How did you get here?");
                        }
                    case 3:
                        switch (true)
                        {
                            case bool ifTrue when
                                validParameters.Contains("cookieName") &&
                                validParameters.Contains("cookieValue") &&
                                validParameters.Contains("siteFrom"):
                                return Ok(
                                    (from c in _context.Cookies
                                     where c.CookieName == cookieName
                                     && c.CookieValue == cookieValue
                                     && c.SiteFrom == siteFrom
                                     select c).FirstOrDefault());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    default:
                        return BadRequest("Incorrect parameters entered");
                }
            //}
            //else
            //{
                return NotFound("Only the BFF application has access to this resource.");
            //}
        }

        /****************************************

        ****************************************/
        [HttpPost("")]
        public async Task<ActionResult> PostCookie([FromBody] Cookie cookie)
        {
            string bffPassword = null == Environment.GetEnvironmentVariable("BFF_PASSWORD")
                                         ? _configuration.GetConnectionString("BFF_PASSWORD")
                                         : Environment.GetEnvironmentVariable("BFF_PASSWORD");
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == bffPassword;
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

            if (isSystem || professor != null)
            {
                try
                {
                    _context.Cookies.Add(cookie);
                    _context.SaveChanges();
                    return Ok(cookie);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }

        /****************************************

        ****************************************/
        [HttpPut("")]
        public async Task<ActionResult> PutCookie([FromBody] Cookie cookie)
        {
            // Gets email from session
            string bffPassword = null == Environment.GetEnvironmentVariable("BFF_PASSWORD")
                                         ? _configuration.GetConnectionString("BFF_PASSWORD")
                                         : Environment.GetEnvironmentVariable("BFF_PASSWORD");
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == bffPassword;
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

            if (isSystem || professor != null)
            {
                try
                {
                    Cookie toModify = (from c in _context.Cookies
                                       where c.CookieId == cookie.CookieId
                                       select c).FirstOrDefault();
                    PropertyInfo[] cookieProperties = cookie.GetType().GetProperties();
                    foreach (PropertyInfo property in cookieProperties)
                    {
                        property.SetValue(cookie, property.GetValue(toModify));
                    }
                    _context.Cookies.Update(toModify);
                    _context.SaveChanges();
                    return Ok(toModify);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }
    }
}
