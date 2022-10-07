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
    public class FolderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VmEntities _context;
        private readonly ILogger<FolderController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FolderController(
            IConfiguration configuration,
            VmEntities context,
            ILogger<FolderController> logger,
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
        public async Task<ActionResult> GetFolder(
            [FromQuery] int? folderId,
            [FromQuery] string vcenterFolderId)
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
                    ("folderId", folderId),
                    ("vcenterFolderId", vcenterFolderId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from f in _context.Folders
                             select f).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "folderId":
                                return Ok(
                                    (from f in _context.Folders
                                     where f.FolderId == folderId
                                     select f).FirstOrDefault());
                            case "vcenterFolderId":
                                return Ok(
                                    (from f in _context.Folders
                                     where f.VcenterFolderId == vcenterFolderId
                                     select f).FirstOrDefault());
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
        public async Task<ActionResult> PostFolder([FromBody] Folder folder)
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
                    _context.Folders.Add(folder);
                    _context.SaveChanges();
                    return Ok(folder);
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
