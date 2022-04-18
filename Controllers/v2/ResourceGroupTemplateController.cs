using DatabaseVmProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseVmProject.Services;
using DatabaseVmProject.Models;
using Database_VmProject.Services;
using System.Linq;

namespace Database_VmProject.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ResourceGroupTemplateController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<ResourceGroupTemplateController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResourceGroupTemplateController(
            VmEntities context,
            ILogger<ResourceGroupTemplateController> logger,
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
        public async Task<ActionResult> GetResourceGroupTemplate(
            [FromQuery] int? resourceGroupTemplateId,
            [FromQuery] double? memory,
            [FromQuery] double? cpu)
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
                    ("resourceGroupTemplateId", resourceGroupTemplateId),
                    ("memory", memory),
                    ("cpu", cpu));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from rgt in _context.ResourceGroupTemplates
                             select rgt).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "resourceGroupTemplateId":
                                return Ok(
                                    (from rgt in _context.ResourceGroupTemplates
                                     where rgt.ResourceGroupTemplateId == resourceGroupTemplateId
                                     select rgt).FirstOrDefault());
                            default:
                                return BadRequest("Incorrect parameters entered");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("memory") &&
                            validParameters.Contains("cpu"):
                                return Ok(
                                    (from rgt in _context.ResourceGroupTemplates
                                     where rgt.Memory.Equals(memory)
                                     && rgt.Cpu.Equals(cpu)
                                     select rgt).FirstOrDefault());
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
        public async Task<ActionResult> PostResourceGroupTemplate([FromBody] ResourceGroupTemplate resourceGroupTemplate)
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
                    _context.ResourceGroupTemplates.Add(resourceGroupTemplate);
                    _context.SaveChanges();
                    return Ok(resourceGroupTemplate);
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
