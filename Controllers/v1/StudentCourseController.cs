using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.DTO.v1;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentCourseController : BeController
    {

        public StudentCourseController(
            IConfiguration configuration,
            ILogger<StudentCourseController> logger,
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
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetCourseListByUserId([FromQuery] int queryUserId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;


            User user = _auth.GetUser();

            if (isSystem || user != null)
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

                                                        select new CourseListByUserDTO(
                                                           s.SectionCanvasId,
                                                           s.SectionId,
                                                           s.SectionName,
                                                           usr.UserSectionRoleId,
                                                           $"{u.FirstName} {u.LastName}"

                                                        )).Distinct().ToList();

                return Ok(courseList);
            }

            else
            {
                return NotFound("You are not Authorized and not a Professor");
            }
        }
    }
}
