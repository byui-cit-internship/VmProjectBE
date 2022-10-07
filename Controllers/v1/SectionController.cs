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
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("sectionList")]
        public async Task<ActionResult> GetSectionListBySemester(string semester)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(userId);

            if (professor != null)
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
