using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;
using System.Linq;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VmEntities _context;
        private readonly ILogger<TagController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TagController(
            IConfiguration configuration,
            VmEntities context,
            ILogger<TagController> logger,
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
        public async Task<ActionResult> GetTag(
            [FromQuery] int? tagId,
            [FromQuery] int? tagCategoryId,
            [FromQuery] string tagVcenterId,
            [FromQuery] string tagName,
            [FromQuery] string tagDescription)
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
                            case "tagName":
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
