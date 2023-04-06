using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;
using VmProjectBE.DTO.v1;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
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

        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetSection(
            [FromQuery] int? sectionId,
            [FromQuery] string sectionName,
            [FromQuery] int? courseId,
            [FromQuery] int? semesterId,
            [FromQuery] int? folderId,
            [FromQuery] int? resourcePoolId,
            [FromQuery] string libraryId,
            [FromQuery] int? sectionNumber,
            [FromQuery] int? sectionCanvasId,
            [FromQuery] int? userSectionRoleId,
            // [FromQuery] int? enrollmentId,
            [FromQuery] int? userId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("sectionId", sectionId),
                    ("sectionName", sectionName),
                    ("courseId", courseId),
                    ("semesterId", semesterId),
                    ("folderId", folderId),
                    ("resourcePoolId", resourcePoolId),
                    ("sectionNumber", sectionNumber),
                    ("sectionCanvasId", sectionCanvasId),
                    ("userSectionRoleId", userSectionRoleId),
                    // ("enrollmentId", enrollmentId),
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
                                    join course in _context.Courses
                                    on s.CourseId equals course.CourseId
                                    where s.SectionId == sectionId
                                    select new{course.CourseCode, s.CourseId, s.LibraryVCenterId, s.FolderId, s.ResourcePoolId, s.SectionCanvasId, s.SectionId, s.SectionName, s.SectionNumber, s.SemesterId}).FirstOrDefault());
                                      
                            case "courseId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.CourseId == courseId
                                     select s).ToList());
                            case "semesterId":
                                return Ok(
                                    (from s in _context.Sections
                                     where s.SemesterId == semesterId
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
                            case "userSectionRoleId":
                                return Ok(
                                    (from s in _context.Sections
                                     join usr in _context.UserSectionRoles
                                     on s.SectionId equals usr.SectionId
                                     where usr.UserSectionRoleId == userSectionRoleId
                                     select s).FirstOrDefault());
                            case "userId":
                                    var userSections = (from s in _context.Sections
                                     join usr in _context.UserSectionRoles
                                     on s.SectionId equals usr.SectionId
                                     join course in _context.Courses
                                     on s.CourseId equals course.CourseId
                                     join vm in _context.VmInstances
                                     on s.SectionId equals vm.SectionId
                                     where usr.UserId == userId
                                     select new SectionDTO(course.CourseCode, s.SectionName, s.SectionId, s.Semester.SemesterTerm, s.SectionNumber, s.SectionName, s.LibraryVCenterId,0)).ToList();

                                    userSections.ForEach( userSection=>{
                                        var vmCount =(from vm in _context.VmInstances where vm.SectionId==userSection.sectionId select vm).Count();


                                        userSection.vmCount=vmCount;
                                    });


                                return Ok(userSections);
                                    
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
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
