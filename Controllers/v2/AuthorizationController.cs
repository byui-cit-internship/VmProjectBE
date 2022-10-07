using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthorizationController : BeController
    {

        public AuthorizationController(
            IConfiguration configuration,
            ILogger<AuthorizationController> logger,
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
            return authType switch
            {
                "admin" => Ok(_auth.GetAdmin()),
                "professor" when sectionId != null => Ok(_auth.GetProfessor((int)sectionId)),
                "user" => Ok(_auth.GetUser()),
                _ => BadRequest("AuthType is required and must be either user, professor, or admin. If 'professor' is used, a sectionID must also be present."),
            };
        }
    }
}
