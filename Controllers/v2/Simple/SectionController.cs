using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<SectionController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SectionController(VmEntities context, ILogger<SectionController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
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
        public async Task<ActionResult> GetSection(
            [FromQuery] int? sectionId,
            [FromQuery] int? courseId,
            [FromQuery] int? semesterId,
            [FromQuery] int? folderId,
            [FromQuery] int? resourceGroupId,
            [FromQuery] int? sectionNumber,
            [FromQuery] int? sectionCanvasId,
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
                    ("sectionId", sectionId),
                    ("courseId", courseId),
                    ("semesterId", semesterId),
                    ("folderId", folderId),
                    ("resourceGroupId", resourceGroupId),
                    ("sectionNumber", sectionNumber),
                    ("sectionCanvasId", sectionCanvasId),
                    ("userId", userId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from s in _context.Sections
                             select s).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "sectionId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.SectionId == sectionId
                                     select s).FirstOrDefault());
                            case "courseId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.CourseId == courseId
                                     select s).ToList());
                            case "semesterId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.CourseId == courseId
                                     select s).ToList());
                            case "folderId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.FolderId == folderId
                                     select s).FirstOrDefault());
                            case "sectionNumber":
                                return Ok(
                                    (from s in _context.Sections
                                           where s.SectionNumber == sectionNumber
                                           select s).ToList());
                            case "sectionCanvasId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.SectionCanvasId == sectionCanvasId
                                     select s).FirstOrDefault());
                            case "userId":
                                return Ok(
                                    (from s in _context.Sections
                                     join usr in _context.UserSectionRoles
                                     on s.SectionId equals usr.SectionId
                                     where usr.UserId == userId
                                     select s).ToList());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when 
                            validParameters.Contains("courseId") &&
                            validParameters.Contains("sectionId"):
                                return Ok(
                                    (from s in _context.Sections
                                     where s.CourseId == courseId
                                     && s.SectionId == sectionId
                                     select s).ToList());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    default:
                        return BadRequest("How did you get here?");
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
        public async Task<ActionResult> PostSection([FromBody] Section section)
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
                    _context.Sections.Add(section);
                    _context.SaveChanges();
                    return Ok(section);
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
