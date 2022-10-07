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
    public class FolderController : BeController
    {

        public FolderController(
            IConfiguration configuration,
            ILogger<FolderController> logger,
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
        public async Task<ActionResult> GetFolder(
            [FromQuery] int? folderId,
            [FromQuery] string vcenterFolderId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
