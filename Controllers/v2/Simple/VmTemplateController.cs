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
    public class VmTemplateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VmEntities _context;
        private readonly ILogger<VmTemplateController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VmTemplateController(
            IConfiguration configuration,
            VmEntities context,
            ILogger<VmTemplateController> logger,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _auth = new(_context, _logger);
            _httpContextAccessor = httpContextAccessor;
            _env = env;
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
            // Gets email from session
            string bffPassword = null == Environment.GetEnvironmentVariable("BFF_PASSWORD")
                                         ? _configuration.GetConnectionString("BFF_PASSWORD")
                                         : Environment.GetEnvironmentVariable("BFF_PASSWORD");
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == bffPassword;
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

            if (isSystem || professor != null)
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
            // Gets email from session
            string bffPassword = null == Environment.GetEnvironmentVariable("BFF_PASSWORD")
                                         ? _configuration.GetConnectionString("BFF_PASSWORD")
                                         : Environment.GetEnvironmentVariable("BFF_PASSWORD");
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == bffPassword;
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

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
