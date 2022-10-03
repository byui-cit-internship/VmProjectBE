using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;
using System.Linq;
using System.Reflection;
using VmProjectBE.DTO;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserSessionController : BeController
    {

        public UserSessionController(
            IConfiguration configuration,
            ILogger<UserSessionController> logger,
            IHttpContextAccessor httpContextAccessor,
            VmEntities context)
            : base(
                  configuration: configuration,
                  httpContextAccessor: httpContextAccessor,
                  logger: logger,
                  context: context)
        {
        }

        /****************************************

        ****************************************/
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult> GetUserSession(
            [FromQuery] int? userId,
            [FromQuery] int? sessionTokenId,
            [FromQuery] int? cookieId,
            [FromQuery] string cookieName,
            [FromQuery] string cookieValue,
            [FromQuery] string siteFrom)
        {
            // Gets email from session
            //bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            //int accessUserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
           // User professor = _auth.getAdmin(accessUserId);
            // Returns a professor user or null if email is not associated with a professor

            //if (isSystem || professor != null)
            //{
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("userId", userId),
                    ("sessionTokenId", sessionTokenId),
                    ("cookieId", cookieId),
                    ("cookieName", cookieName),
                    ("cookieValue", cookieValue),
                    ("siteFrom", siteFrom));
                switch (validParameters.Count)
                {
                    case 0:
                        return BadRequest("Must provide at least one parameter");
                    case 1:
                        switch (validParameters[0])
                        {
                            case "userId":
                                return Ok(
                                    (from u in _context.Users
                                     join at in _context.AccessTokens
                                     on u.UserId equals at.UserId
                                     join st in _context.SessionTokens
                                     on at.AccessTokenId equals st.AccessTokenId
                                     where u.UserId == userId
                                     select new UserSession(
                                         u,
                                         st)).ToList());
                            case "sessionTokenId":
                                return Ok(
                                    (from st in _context.SessionTokens
                                     join at in _context.AccessTokens
                                     on st.AccessTokenId equals at.AccessTokenId
                                     join u in _context.Users
                                     on at.UserId equals u.UserId
                                     where st.SessionTokenId == sessionTokenId
                                     select new UserSession(
                                         u,
                                         st)).FirstOrDefault());
                            case "cookieId":
                                return Ok(
                                    (from c in _context.Cookies
                                     join st in _context.SessionTokens
                                     on c.SessionTokenId equals st.SessionTokenId
                                     join at in _context.AccessTokens
                                     on st.AccessTokenId equals at.AccessTokenId
                                     join u in _context.Users
                                     on at.UserId equals u.UserId
                                     where c.CookieId == cookieId
                                     select new UserSession(
                                         u,
                                         st)).FirstOrDefault());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
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
                                     join st in _context.SessionTokens
                                     on c.SessionTokenId equals st.SessionTokenId
                                     join at in _context.AccessTokens
                                     on st.AccessTokenId equals at.AccessTokenId
                                     join u in _context.Users
                                     on at.UserId equals u.UserId
                                     where c.CookieName == cookieName
                                     && c.CookieValue == cookieValue
                                     && c.SiteFrom == siteFrom
                                     select new UserSession(
                                         u,
                                         st)).FirstOrDefault());
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
    }
}
