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
    public class TagController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<TagController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TagController(
            VmEntities context,
            ILogger<TagController> logger,
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
        public async Task<ActionResult> GetTag(
            [FromQuery] int? tagId,
            [FromQuery] int? tagCategoryId,
            [FromQuery] string tagVcenterId,
            [FromQuery] string tagName,
            [FromQuery] string tagDescription)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(userId);
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("tagId", tagId),
                    ("tagCategoryId", tagCategoryId),
                    ("tagVcenterId", tagVcenterId),
                    ("tagName", tagName),
                    ("tagDescription", tagDescription));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from t in _context.Tags
                             select t).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "tagId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagId == tagId
                                     select t).FirstOrDefault());
                            case "tagCategoryId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagCategoryId == tagCategoryId
                                     select t).ToList());
                            case "tagVcenterId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagVcenterId == tagVcenterId
                                     select t).FirstOrDefault());
                            case "tagNAme":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagName == tagName
                                     select t).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                                validParameters.Contains("tagCategoryId") &&
                                validParameters.Contains("tagName"):
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagCategoryId == tagCategoryId
                                     && t.TagName == tagName
                                     select t).FirstOrDefault());
                            default:
                                return BadRequest("Incorrect parameters entered");
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
        public async Task<ActionResult> PostTag([FromBody] Tag tag)
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
                    _context.Tags.Add(tag);
                    _context.SaveChanges();
                    return Ok(tag);
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
