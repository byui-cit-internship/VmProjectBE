using DatabaseVmProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseVmProject.Services;
using DatabaseVmProject.Models;
using Database_VmProject.Services;
using System.Linq;

namespace DatabaseVmProject.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class TagUserController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<TagUserController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TagUserController(
            VmEntities context,
            ILogger<TagUserController> logger,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {

            _context = context;
            _logger = logger;
            _auth = new(_context, _logger);
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        /****************************************

        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetTagUser(
            [FromQuery] int? tagUserId,
            [FromQuery] int? tagId,
            [FromQuery] int? userId)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int accessUserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(accessUserId);
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("tagUserId", tagUserId),
                    ("tagId", tagId),
                    ("userId", userId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from tu in _context.TagUsers
                             select tu).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "tagUserId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagUserId == tagUserId
                                     select tu).FirstOrDefault());
                            case "tagId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagId == tagId
                                     select tu).ToList());
                            case "userId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.UserId == userId
                                     select tu).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("tagId") &&
                            validParameters.Contains("userId"):
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagId == tagId
                                     && tu.UserId == userId
                                     select tu).FirstOrDefault());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    default:
                        return BadRequest("Incorrect parameters entered");
                }
            }
            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }

        /****************************************

        ****************************************/
        [HttpPost("")]
        public async Task<ActionResult> PostTagUser([FromBody] TagUser tagUser)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(userId);
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || professor != null)
            {
                try
                {
                    _context.TagUsers.Add(tagUser);
                    _context.SaveChanges();
                    return Ok(tagUser);
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
