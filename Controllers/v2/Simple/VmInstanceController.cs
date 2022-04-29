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
    public class VmInstanceController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<VmInstanceController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VmInstanceController(
            VmEntities context,
            ILogger<VmInstanceController> logger,
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
        public async Task<ActionResult> GetVmTemplate(
            [FromQuery] int? vmInstanceId,
            [FromQuery] int? vmTemplateId,
            [FromQuery] string vmInstanceVcenterId,
            [FromQuery] DateTime? vmInstanceExpireDate)
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
                    ("vmInstanceId", vmInstanceId),
                    ("vmTemplateId", vmTemplateId),
                    ("vmInstanceVcenterId", vmInstanceVcenterId),
                    ("vmInstanceExpireDate", vmInstanceExpireDate));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from vi in _context.VmInstances
                             select vi).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "vmInstanceId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmInstanceId == vmInstanceId
                                     select vi).FirstOrDefault());
                            case "vmTemplateId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmTemplateId == vmTemplateId
                                     select vi).ToList());
                            case "vmInstanceVcenterId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmInstanceVcenterId == vmInstanceVcenterId
                                     select vi).FirstOrDefault());
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
        public async Task<ActionResult> PostVmInstance([FromBody] VmInstance vmInstance)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User user = _auth.getUser(userId);
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || user != null)
            {
                try
                {
                    _context.VmInstances.Add(vmInstance);
                    _context.SaveChanges();
                    return Ok(vmInstance);
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
