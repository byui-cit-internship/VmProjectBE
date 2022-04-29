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
    public class CreateVmController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<CreateVmController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateVmController(VmEntities context, ILogger<CreateVmController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
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
        public async Task<ActionResult> GetCanvasUsers([FromQuery] int enrollmentId)
        {
            // Gets email from session
            // Returns a professor user or null if email is not associated with a professor

            // Returns a list of course name, section id, semester, section number, and professor
            // based on the professor and semester variables
            List<CreateVmDTO> createVm = (from usr in _context.UserSectionRoles
                                          where usr.UserSectionRoleId == enrollmentId
                                          join u in _context.Users
                                          on usr.UserId equals u.UserId
                                          join sec in _context.Sections
                                          on usr.SectionId equals sec.SectionId
                                          join f in _context.Folders
                                          on sec.FolderId equals f.FolderId
                                          join c in _context.Courses
                                          on sec.CourseId equals c.CourseId
                                          join sem in _context.Semesters
                                          on sec.SemesterId equals sem.SemesterId
                                          join t in _context.Tags
                                          on c.CourseCode equals t.TagName
                                          join tc in _context.TagCategories
                                          on t.TagCategoryId equals tc.TagCategoryId
                                          join vtt in _context.VmTemplateTags
                                          on t.TagId equals vtt.TagId
                                          join vt in _context.VmTemplates
                                          on vtt.VmTemplateId equals vt.VmTemplateId
                                          where usr.UserSectionRoleId == enrollmentId
                                          && tc.TagCategoryName == "Course"
                                          select new CreateVmDTO(
                                              $"{u.FirstName} {u.LastName}",
                                              c.CourseName,
                                              c.CourseId,
                                              vt.VmTemplateVcenterId,
                                              sem.SemesterTerm,
                                              usr.UserSectionRoleId,
                                              f.VcenterFolderId
                                          )).ToList();

            return Ok(createVm);
        }

        [HttpPost("")]
        public async Task<ActionResult> PostCreateVm([FromBody] VmInstance vmInstance)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            // Gets email from session
            User user = _auth.getUser(userId);
            try
            {
                TagCategory userTC = (from tc in _context.TagCategories
                                      where tc.TagCategoryName == "User"
                                      select tc).FirstOrDefault();
                if (userTC == null)
                {
                    userTC = new();
                    userTC.TagCategoryName = "User";
                    userTC.TagCategoryVcenterId = "DONT_USE";
                    userTC.TagCategoryDescription = "For temporary use.";

                    _context.TagCategories.Add(userTC);
                    _context.SaveChanges();
                }

                Tag userTag = (from t in _context.Tags
                               where t.TagName == user.Email
                               select t).FirstOrDefault();

                if (userTag == null)
                {
                    userTag = new();
                    userTag.TagName = user.Email;
                    userTag.TagCategoryId = userTC.TagCategoryId;
                    userTag.TagVcenterId = "DONT_USE";

                    _context.Tags.Add(userTag);
                    _context.SaveChanges();
                }

                TagUser tagUser = (from tu in _context.TagUsers
                                   where tu.UserId == user.UserId
                                   && tu.TagId == userTag.TagId
                                   select tu).FirstOrDefault();

                if (tagUser == null)
                {
                    tagUser = new();
                    tagUser.TagId = userTag.TagId;
                    tagUser.UserId = user.UserId;

                    _context.TagUsers.Add(tagUser);
                    _context.SaveChanges();
                }

                _context.VmInstances.Add(vmInstance);
                _context.SaveChanges();

                VmInstanceTag vmInstanceTag = new();
                vmInstanceTag.VmInstanceId = vmInstance.VmInstanceId;
                vmInstanceTag.TagId = userTag.TagId;

                _context.VmInstanceTags.Add(vmInstanceTag);
                _context.SaveChanges();

                return Ok(vmInstance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
