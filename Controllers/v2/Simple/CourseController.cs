using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CourseController : BeController
    {

        public CourseController(
            IConfiguration configuration,
            ILogger<CourseController> logger,
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
        public async Task<ActionResult> GetCourse(
            [FromQuery] int? courseId,
            [FromQuery] string courseCode,
            [FromQuery] int? resourcePoolTemplateId,
            [FromQuery] int? vmTemplateId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("courseId", courseId),
                    ("courseCode", courseCode),
                    ("resourcePoolTemplateId", resourcePoolTemplateId),
                    ("vmTemplateId", vmTemplateId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from c in _context.Courses
                             select c).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "courseId":
                                return Ok(
                                    (from c in _context.Courses
                                     where c.CourseId == courseId
                                     select c).FirstOrDefault());
                            case "courseCode":
                                return Ok(
                                    (from c in _context.Courses
                                     where c.CourseCode == courseCode
                                     select c).FirstOrDefault());
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
        public async Task<ActionResult> PostCourse([FromBody] Course course)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                try
                {
                    _context.Courses.Add(course);
                    _context.SaveChanges();
                    return Ok(course);
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
