using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using DatabaseVmProject.DAL;
using DatabaseVmProject.Models;


namespace DatabaseVmProject.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<TokenController> _logger;

        public IHttpClientFactory _httpClientFactory { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenController(
            VmEntities context,
            ILogger<TokenController> logger,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /**************************************
        Validate the token given by the front end
        and then determines whether they are a teacher or professor
        ****************************************/
        [HttpPost()]
        [AllowAnonymous]
        public async Task<ActionResult> PostToken([FromBody] DTO.AccessToken accessTokenObj)
        {
            try
            {
                DTO.AccessToken backendCookie = new();
                backendCookie.CookieName = ".VMProject.Session";
                backendCookie.CookieValue = _httpContextAccessor.HttpContext.Request.Cookies[backendCookie.CookieName];
                backendCookie.SiteFrom = "BE";

                if (backendCookie.CookieValue == null)
                {
                    return StatusCode(500, "Session cookie not set. Try again.");
                }

                if (accessTokenObj.AccessTokenValue == Environment.GetEnvironmentVariable("BFF_PASSWORD"))
                {
                    _httpContextAccessor.HttpContext.Session.SetString("tokenId", Environment.GetEnvironmentVariable("BFF_PASSWORD"));
                    return Ok();
                }

                HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {accessTokenObj.AccessTokenValue}");
                HttpResponseMessage response = await httpClient.GetAsync($"https://www.googleapis.com/userinfo/v2/me");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    dynamic students = JsonConvert.DeserializeObject<dynamic>(responseString);

                    string email = students.email;

                    User user = (from u in _context.Users
                                 where u.Email == email
                                 select u).FirstOrDefault();

                    if (user == null)
                    {

                        user = new User();
                        user.Email = email;
                        user.FirstName = students.given_name;
                        user.LastName = students.family_name;
                        user.IsAdmin = false;

                        _context.Users.Add(user); ;
                        _context.SaveChanges();

                    }

                    AccessToken accessToken = (from at in _context.AccessTokens
                                               where at.AccessTokenValue == accessTokenObj.AccessTokenValue
                                               select at).FirstOrDefault();
                    if (accessToken == null)
                    {
                        accessToken = new();
                        accessToken.AccessTokenValue = accessTokenObj.AccessTokenValue;
                        accessToken.ExpireDate = DateTime.Now.AddHours(1);
                        accessToken.User = user;

                        _context.AccessTokens.Add(accessToken);
                        _context.SaveChanges();
                    }
                    else if (DateTime.Compare(accessToken.ExpireDate, DateTime.Now) < 0)
                    {
                        return Forbid();
                    }


                    SessionToken sessionToken = (from st in _context.SessionTokens
                                                 where st.AccessToken == accessToken
                                                 orderby st.ExpireDate descending
                                                 select st).FirstOrDefault();
                    if (sessionToken == null)
                    {
                        sessionToken = new();
                        sessionToken.SessionTokenValue = sessionToken.SessionTokenValue = Guid.NewGuid();
                        sessionToken.AccessToken = accessToken;
                        sessionToken.ExpireDate = DateTime.Now.AddDays(3000);

                        _context.SessionTokens.Add(sessionToken);
                        _context.SaveChanges();
                    }
                    else if (DateTime.Compare(sessionToken.ExpireDate, DateTime.Now) < 0)
                    {
                        return Forbid();
                    }

                    Cookie beCookie = new();
                    beCookie.CookieName = backendCookie.CookieName;
                    beCookie.CookieValue = backendCookie.CookieValue;
                    beCookie.SiteFrom = backendCookie.SiteFrom;
                    beCookie.SessionTokenId = sessionToken.SessionTokenId;
                    _context.Cookies.Add(beCookie);

                    Cookie bffCookie = new();
                    bffCookie.CookieName = accessTokenObj.CookieName;
                    bffCookie.CookieValue = accessTokenObj.CookieValue;
                    bffCookie.SiteFrom = accessTokenObj.SiteFrom;
                    bffCookie.SessionTokenId = sessionToken.SessionTokenId;

                    _context.Cookies.Add(bffCookie);
                    _context.SaveChanges();

                    _httpContextAccessor.HttpContext.Session.SetString("BESessionCookie", $"{beCookie.CookieName}={beCookie.CookieValue}");
                    _httpContextAccessor.HttpContext.Session.SetString("sessionTokenValue", sessionToken.SessionTokenValue.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("userId", user.UserId.ToString());

                    // outside return statment
                    return Ok((user, sessionToken.SessionTokenValue.ToString()));
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500);

            }
        }

        /**************************************
        Takes a session token and finds the user associated with it
        and returns it.
        ****************************************/
        [HttpGet()]
        public async Task<ActionResult> GetToken()
        {
            try
            {
                string sessionId = _httpContextAccessor.HttpContext.Session.GetString("tokenId");

                User user = (from st in _context.SessionTokens
                             join at in _context.AccessTokens
                             on st.AccessTokenId equals at.AccessTokenId
                             join u in _context.Users
                             on at.UserId equals u.UserId
                             where st.SessionTokenValue == Guid.Parse(sessionId)
                             select u).FirstOrDefault();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500);

            }
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".VMProject.Session");
            return Ok();
        }
    }
}
