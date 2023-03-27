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
    public class VmTemplateTagController : BeController
    {

        public VmTemplateTagController(
            IConfiguration configuration,
            ILogger<VmTemplateTagController> logger,
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
        public async Task<ActionResult> GetVmTemplateTag(
            [FromQuery] int? vmTemplateTagId,
            [FromQuery] int? tagId,
            [FromQuery] string vmTemplateId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
