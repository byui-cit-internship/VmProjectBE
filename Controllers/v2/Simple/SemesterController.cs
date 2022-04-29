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
    public class SemesterController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<SemesterController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SemesterController(
            VmEntities context,
            ILogger<SemesterController> logger,
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
        public async Task<ActionResult> GetSemester(
            [FromQuery] int? semesterId,
            [FromQuery] int? semesterYear,
            [FromQuery] string semesterTerm,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
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
                    ("semesterId", semesterId),
                    ("semesterYear", semesterYear),
                    ("semesterTerm", semesterTerm));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from s in _context.Semesters
                             select s).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "semesterId":
                                return Ok(
                                    (from s in _context.Semesters
                                     where s.SemesterId == semesterId
                                     select s).FirstOrDefault());
                            case "semesterYear":
                                return Ok(
                                    (from s in _context.Semesters
                                     where s.SemesterId == semesterId
                                     select s).FirstOrDefault());
                            default:
                                return BadRequest("Incorrect parameters entered");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when 
                                validParameters.Contains("semesterYear") &&
                                validParameters.Contains("semesterTerm"):
                                return Ok(
                                    (from s in _context.Semesters
                                     where s.SemesterYear == semesterYear
                                     && s.SemesterTerm == semesterTerm
                                     select s).FirstOrDefault());
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
        public async Task<ActionResult> PostSemester([FromBody] Semester semester)
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
                    _context.Semesters.Add(semester);
                    _context.SaveChanges();
                    return Ok(semester);
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
