using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VmProjectBE.DAL;
using VmProjectBE.DTO;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : BeController
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public TokenController(
            IConfiguration configuration,
            ILogger<TokenController> logger,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            VmEntities context)
            : base(
                  configuration: configuration,
                  httpContextAccessor: httpContextAccessor,
                  logger: logger,
                  context: context)
        {
            HttpClientFactory = httpClientFactory;
        }

        /**************************************
        Validate the token given by the front end
        and then determines whether they are a teacher or professor
        ****************************************/
        [HttpPost()]
        [AllowAnonymous]
        public async Task<ActionResult> PostToken([FromBody] AccessTokenDTO accessTokenObj)
        {
            try
            {   
                if (accessTokenObj.AccessTokenValue == _configuration.GetConnectionString("BFF_PASSWORD"))
                {
                    return Ok();
                }

                HttpClient httpClient = HttpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={accessTokenObj.AccessTokenValue}");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    dynamic googleUser = JsonConvert.DeserializeObject<dynamic>(responseString);

                    string email = googleUser.email;

                    User user = (from u in _context.Users
                                 where u.Email == email
                                 select u).FirstOrDefault();

                    if (user == null)
                    {
                        user = new User();
                        user.Email = email;
                        user.FirstName = googleUser.given_name;
                        user.LastName = googleUser.family_name;
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

                    _httpContextAccessor.HttpContext.Response.Cookies.Append(
                        "vima-cookie",
                        $"{sessionToken.SessionTokenValue}",
                        new CookieOptions() { SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

                    // outside return statment
                    return Ok((user, sessionToken.SessionTokenValue.ToString()));
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Call to google to validate accessToken failed with status code: {(int)response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed with error: {ex.Message}");

            }
        }

        /**************************************
        Takes a session token and finds the user associated with it
        and returns it.
        ****************************************/
        [HttpGet()]
        public async Task<ActionResult> GetToken([FromQuery] string sessionToken)
        {
            try
            {
                User user = (from st in _context.SessionTokens
                             join at in _context.AccessTokens
                             on st.AccessTokenId equals at.AccessTokenId
                             join u in _context.Users
                             on at.UserId equals u.UserId
                             where st.SessionTokenValue == Guid.Parse(sessionToken)
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
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("vima-cookie");
            return Ok();
        }
    }
}
