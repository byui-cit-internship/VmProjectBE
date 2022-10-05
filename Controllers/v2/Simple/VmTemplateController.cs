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
    public class VmTemplateController : BeController
    {

        public VmTemplateController(
            IConfiguration configuration,
            ILogger<VmTemplateController> logger,
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
        public async Task<ActionResult> GetVmTemplate(
            [FromQuery] int? vmTemplateId,
            [FromQuery] string vmTemplateVcenterId,
            [FromQuery] string vmTemplateName,
            [FromQuery] DateTime? vmTemplateAccessDate)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("vmTemplateId", vmTemplateId),
                    ("vmTemplateVcenterId", vmTemplateVcenterId),
                    ("vmTemplateName", vmTemplateName),
                    ("vmTemplateAccessDate", vmTemplateAccessDate));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from vt in _context.VmTemplates
                             select vt).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "vmTemplateId":
                                return Ok(
                                    (from vt in _context.VmTemplates
                                     where vt.VmTemplateId == vmTemplateId
                                     select vt).FirstOrDefault());
                            case "vmTemplateVcenterId":
                                return Ok(
                                    (from vt in _context.VmTemplates
                                     where vt.VmTemplateVcenterId == vmTemplateVcenterId
                                     select vt).FirstOrDefault());
                            case "vmTemplateName":
                                return Ok(
                                    (from vt in _context.VmTemplates
                                     where vt.VmTemplateName == vmTemplateName
                                     select vt).FirstOrDefault());
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
        public async Task<ActionResult> PostVmTemplate([FromBody] VmTemplate vmTemplate)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.VmTemplates.Add(vmTemplate);
                    _context.SaveChanges();
                    return Ok(vmTemplate);
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
