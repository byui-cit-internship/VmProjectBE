using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationController(
            VmEntities context, 
            ILogger<AuthorizationController> logger,
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
        /** 
        * <summary>
        * Authorize user based on requested 'authType' and if a professor, 'sectionId'. 
        * </summary>
        * <param name="authType">Type of authorization current user is seeking. Valid types include 'admin', 'professor', and 'user'</param>
        * <param name="sectionId">ID of the section a professor is seeking authorization for. Only used if 'professor' is specified in 'authType'.</param>
        * <returns>A "User" object</returns>
        * <remarks>
        * Only certain parameter combinations are allowed. Possible combinations include:<br/>
        * <![CDATA[
        *      <pre>
        *          <code>/authorization?authType=admin
        *                /authorization?authType=professor&sectionId={sectionId}
        *                /authorization?authType=user
        *          </code>
        *      </pre>
        * ]]>
        * Sample requests:
        *
        *      Returns a user object if the requesting user is an admin.
        *      GET /user?authType=admin
        *      RETURNS
        *      {
        *          "userId": 1,
        *          "firstName": "Michael",
        *          "lastName": "Ebenal",
        *          "email": "ebe17003@byui.edu",
        *          "is_admin": true,
        *          "canvas_token": null
        *      }
        *      
        *      Returns a user object if the requesting user is a professor.
        *      GET /user?authType=professor&amp;sectionId=1
        *      RETURNS
        *      {
        *          "userId": 1,
        *          "firstName": "Michael",
        *          "lastName": "Ebenal",
        *          "email": "ebe17003@byui.edu",
        *          "is_admin": true,
        *          "canvas_token": null
        *      }
        *      
        *      Returns a user object if the requesting user is an admin.
        *      GET /user?authType=user
        *      RETURNS
        *      {
        *          "userId": 1,
        *          "firstName": "Michael",
        *          "lastName": "Ebenal",
        *          "email": "ebe17003@byui.edu",
        *          "is_admin": true,
        *          "canvas_token": null
        *      }
        *
        * </remarks>
        * <response code="200">Returns a user object</response>
        * <response code="400">Incorrect parameters/combination entered</response>
        */
        [HttpGet()]
        public async Task<ActionResult> AuthorizeUsers(
            [FromQuery] string authType,
            [FromQuery] int? sectionId = null)
        {
            int userId = 0;
            if ((_httpContextAccessor.HttpContext.Session.GetString("userId")) == null) {
                userId = 1;
            } else {
                userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            }
            switch (authType)
            {
                case "admin":
                    return Ok(_auth.getAdmin(userId));
                case "professor" when sectionId != null:
                    return Ok(_auth.getProfessor(userId, (int)sectionId));
                case "user":
                    return Ok(_auth.getUser(userId));
                default:
                    return BadRequest("AuthType is required and must be either user, professor, or admin. If 'professor' is used, a sectionID must also be present.");
            }
        }
    }
}
