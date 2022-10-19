using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.DTO.v1;
using VmProjectBE.Models;
using Newtonsoft.Json;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SectionController : BeController
    {

        public SectionController(
            IConfiguration configuration,
            ILogger<SectionController> logger,
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
        [HttpGet("sectionList")]
        public async Task<ActionResult> GetSectionListBySemester([FromQuery] string semester)
        {
            _logger.LogInformation(semester);
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;


            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                // Returns a list of course name, section id, semester, section number, and professor
                // based on the professor and semester variables
                List<SectionDTO> sectionList = (from c in _context.Courses
                                                join sec in _context.Sections
                                                on c.CourseId equals sec.CourseId
                                                join sem in _context.Semesters
                                                on sec.SemesterId equals sem.SemesterId
                                                join usr in _context.UserSectionRoles
                                                on sec.SectionId equals usr.SectionId
                                                join u in _context.Users
                                                on usr.UserId equals u.UserId
                                                where u.Email == professor.Email
                                                && sem.SemesterTerm == semester
                                                select new SectionDTO(
                                                    c.CourseCode,
                                                    c.CourseName,
                                                    sec.SectionId,
                                                    sem.SemesterTerm,
                                                    sec.SectionNumber,
                                                    $"{u.FirstName} {u.LastName}"
                                                )).ToList();

                return Ok(sectionList);
            }

            else
            {
                return NotFound("You are not Authorized and not a Professor");
            }
        }
    }
}
