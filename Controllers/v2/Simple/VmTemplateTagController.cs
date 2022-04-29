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
    public class VmTemplateTagController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<VmTemplateTagController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VmTemplateTagController(
            VmEntities context,
            ILogger<VmTemplateTagController> logger,
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
        public async Task<ActionResult> GetVmTemplateTag(
            [FromQuery] int? vmTemplateTagId,
            [FromQuery] int? tagId,
            [FromQuery] int? vmTemplateId)
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
                    ("vmTemplateTagId", vmTemplateTagId),
                    ("tagId", tagId),
                    ("vmTemplateId", vmTemplateId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from vtt in _context.VmTemplateTags
                             select vtt).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "vmTemplateTag":
                                return Ok(
                                    (from vtt in _context.VmTemplateTags
                                     where vtt.VmTemplateTagId == vmTemplateTagId
                                     select vtt).FirstOrDefault());
                            case "tagId":
                                return Ok(
                                    (from vtt in _context.VmTemplateTags
                                     where vtt.TagId == tagId
                                     select vtt).ToList());
                            case "vmTemplateId":
                                return Ok(
                                    (from vtt in _context.VmTemplateTags
                                     where vtt.VmTemplateId == vmTemplateId
                                     select vtt).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("tagId") &&
                            validParameters.Contains("vmTemplateId"):
                                return Ok(
                                    (from vtt in _context.VmTemplateTags
                                     where vtt.TagId == tagId
                                     && vtt.VmTemplateId == vmTemplateId
                                     select vtt).FirstOrDefault());
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
        public async Task<ActionResult> PostVmTemplateTag([FromBody] VmTemplateTag vmTemplateTag)
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
                    _context.VmTemplateTags.Add(vmTemplateTag);
                    _context.SaveChanges();
                    return Ok(vmTemplateTag);
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
