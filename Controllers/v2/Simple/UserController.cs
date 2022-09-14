using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;
using System.Linq;
using System.Reflection;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<UserController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(
            VmEntities context,
            ILogger<UserController> logger,
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
         * Gets a user's info. 
         * </summary>
         * <param name="userId">Primary key of a user.</param>
         * <param name="firstName">First name of the user being requested.</param>
         * <param name="lastName">Last name of the user being requested.</param>
         * <param name="email">Email of the user being requested.</param>
         * <param name="isAdmin">Whether the user is an administrator or not.</param>
         * <param name="canvasToken">Specified when the user is a professor.</param>
         * <returns>A "User" object(s)</returns>
         * <remarks>
         * Only certain parameter combinations are allowed. Possible combinations include:<br/>
         * <![CDATA[
         *      <pre>
         *          <code>/user
         *                /user?userId={userId}
         *                /user?email={email}
         *                /user?canvasToken={canvasToken}
         *          </code>
         *      </pre>
         * ]]>
         * Sample requests:
         *
         *      Returns all users.
         *      GET /user
         *      RETURNS
         *      {
         *          [
         *              {
         *                  "userId": 1,
         *                  "firstName": "Michael",
         *                  "lastName": "Ebenal",
         *                  "email": "ebe17003@byui.edu",
         *                  "is_admin": true,
         *                  "canvas_token": null
         *              },
         *              {
         *                  "userId": 2,
         *                  "firstName": "Jaren",
         *                  "lastName": "Brownlee",
         *                  "email": "bro14001@byui.edu",
         *                  "isAdmin: false,
         *                  "canvasToken": null
         *              },
         *              ...
         *          ]
         *      }
         *
         *      Returns user with specified ID. Note: Not student/faculty ID but database primary key.
         *      GET /user?userId=1
         *      RETURNS
         *      {
         *          "userId": 1,
         *          "firstName": "Michael",
         *          "lastName": "Ebenal",
         *          "email" : "ebe17003@byui.edu",
         *          "is_admin" : "true",
         *          "canvas_token" : "null"
         *      }
         * 
         *      Returns user with specified email.
         *      GET /user?email=ebe17003%40byui.edu
         *      RETURNS
         *      {
         *          "userId": 1,
         *          "firstName": "Michael",
         *          "lastName": "Ebenal",
         *          "email" : "ebe17003@byui.edu",
         *          "is_admin" : "true",
         *          "canvas_token" : "null"
         *      }
         *
         * </remarks>
         * <response code="200">Returns a user object(s)</response>
         * <response code="400">Incorrect parameters/combination entered</response>
         * <response code="403">Insufficent permissions to make request</response>
         */
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> GetUser(
            [FromQuery] int? userId,
            [FromQuery] string firstName,
            [FromQuery] string lastName,
            [FromQuery] string email,
            [FromQuery] bool? isAdmin,
            [FromQuery] string canvasToken)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");
            int accessUserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(accessUserId);
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("userId", userId),
                    ("firstName", firstName),
                    ("lastName", lastName),
                    ("email", email),
                    ("isAdmin", isAdmin),
                    ("canvasToken", canvasToken));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from u in _context.Users
                             select u).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "userId":
                                return Ok(
                                    (from u in _context.Users
                                     where u.UserId == userId
                                     select u).FirstOrDefault());
                            case "email":
                                return Ok(
                                    (from u in _context.Users
                                     where u.Email == email
                                     select u).FirstOrDefault());
                            case "canvasToken":
                                return Ok(
                                    (from u in _context.Users
                                     where u.CanvasToken == canvasToken
                                     select u).FirstOrDefault());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    default:
                        return BadRequest("Incorrect parameters entered");
                }
            }
            else
            {
                return Forbid("Only authorized users can access data about users");
            }
        }

        /****************************************

        ****************************************/
        /** 
         * <summary>
         * Creates a new user 
         * </summary>
         * <returns>The created "User" object</returns>
         * <remarks>
         * Sample requests:
         *
         *      Creates a single user.
         *      POST /user
         *      BODY
         *      {
         *          "firstName": "Michael",
         *          "lastName": "Ebenal",
         *          "email": "ebe17003@byui.edu"
         *          "is_admin": true
         *      }
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
         *      POST /user
         *      BODY
         *      {
         *          "firstName": "Michael",
         *          "lastName": "Ebenal",
         *          "email": "ebe17003@byui.edu"
         *          "is_admin": true,
         *          "canvas_token": "asjhdgjhgsjdfhgjagsdjkhgkjgakjfg"
         *      }
         *      RETURNS
         *      {
         *          "userId": 1,
         *          "firstName": "Michael",
         *          "lastName": "Ebenal",
         *          "email": "ebe17003@byui.edu",
         *          "is_admin": true,
         *          "canvas_token": "asjhdgjhgsjdfhgjagsdjkhgkjgakjfg"
         *      }
         *
         *      
         * </remarks>
         * <response code="200">Returns a created user object</response>
         * <response code="400">General error message, likely an incorrect body or database availability problem</response>
         * <response code="403">Insufficent permissions to make request</response>
         */
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> PostUser([FromBody] User user)
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
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Forbid("Only the BFF application has access to this resource.");
            }
        }

        /****************************************

        ****************************************/
        [HttpPut("")]
        public async Task<ActionResult> PutUser([FromBody] User user)
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
                    User toModify = _context.Users.Find(user.UserId);
                    PropertyInfo[] userProperties = user.GetType().GetProperties();
                    foreach (PropertyInfo property in userProperties)
                    {
                        property.SetValue(toModify, property.GetValue(user));
                    }
                    _context.Users.Update(toModify);
                    _context.SaveChanges();
                    return Ok(user);
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
