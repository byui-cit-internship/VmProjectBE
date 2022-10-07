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
    public class TagCategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VmEntities _context;
        private readonly ILogger<TagCategoryController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TagCategoryController(
            IConfiguration configuration,
            VmEntities context,
            ILogger<TagCategoryController> logger,
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
        public async Task<ActionResult> GetTagCategory(
            [FromQuery] int? tagCategoryId,
            [FromQuery] string tagCategoryVcenterId,
            [FromQuery] string tagCategoryName,
            [FromQuery] string tagCategoryDescription)
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
                    ("tagCategoryId", tagCategoryId),
                    ("tagCategoryVcenterId", tagCategoryVcenterId),
                    ("tagCategoryName", tagCategoryName),
                    ("tagCategoryDescription", tagCategoryDescription));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from tc in _context.TagCategories
                             select tc).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "tagCategoryId":
                                return Ok(
                                    (from tc in _context.TagCategories
                                     where tc.TagCategoryId == tagCategoryId
                                     select tc).FirstOrDefault());
                            case "tagCategoryVcenterId":
                                return Ok(
                                    (from tc in _context.TagCategories
                                     where tc.TagCategoryVcenterId == tagCategoryVcenterId
                                     select tc).FirstOrDefault());
                            case "tagCategoryName":
                                return Ok(
                                    (from tc in _context.TagCategories
                                     where tc.TagCategoryName == tagCategoryName
                                     select tc).FirstOrDefault());
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
        public async Task<ActionResult> PostTagCategory([FromBody] TagCategory tagCategory)
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
                    _context.TagCategories.Add(tagCategory);
                    _context.SaveChanges();
                    return Ok(tagCategory);
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
