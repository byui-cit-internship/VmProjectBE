using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Services;

namespace VmProjectBE.Controllers
{
    public class BeController : ControllerBase
    {
        protected readonly Authorization _auth;
        protected readonly IConfiguration _configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger _logger;
        protected readonly string _vimaCookie;
        protected readonly VmEntities _context;

        public BeController(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ILogger logger,
            VmEntities context)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("vima-cookie", out _vimaCookie);
            _auth = new(
                configuration: _configuration,
                context: _context,
                logger: _logger,
                vimaCookie: _vimaCookie);
        }
    }
}
