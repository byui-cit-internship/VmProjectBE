using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using VmProjectBE.DTO.v1;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<StudentCourseController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentCourseController(
            VmEntities context, 
            ILogger<StudentCourseController> logger,
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
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetCourseListByUserId([FromQuery] int queryUserId)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User user = _auth.getUser(userId);

            if (user != null)
            {
                // Returns a list of course name, section id, semester, section number, and professor
                // based on the professor and semester variables
                List<CourseListByUserDTO> courseList = (from u in _context.Users
                                                        join usr in _context.UserSectionRoles
                                                        on u.UserId equals usr.UserId
                                                        join s in _context.Sections
                                                        on usr.SectionId equals s.SectionId
                                                        join c in _context.Courses
                                                        on s.CourseId equals c.CourseId
                                                        join t in _context.Tags
                                                        on c.CourseName equals t.TagName
                                                        join tc in _context.TagCategories
                                                        on t.TagCategoryId equals tc.TagCategoryId
                                                        join vtt in _context.VmTemplateTags
                                                        on t.TagId equals vtt.TagId
                                                        join vt in _context.VmTemplates
                                                        on vtt.VmTemplateId equals vt.VmTemplateId
                                                        where tc.TagCategoryName == "Course"
                                                        && t.TagName == c.CourseCode
                                                        && u.UserId == queryUserId
                                                select new CourseListByUserDTO(
                                                   s.SectionCanvasId,
                                                   c.CourseName,
                                                   usr.UserSectionRoleId,
                                                   $"{u.FirstName} {u.LastName}",
                                                   tc.TagCategoryVcenterId
                                                )).ToList();

                return Ok(courseList);
            }

            else
            {
                return NotFound("You are not Authorized and not a Professor");
            }
        }
    }
}
