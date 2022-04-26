using FinalProjectContemporaryProgramming.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
namespace FinalProjectContemporaryProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MattCController : ControllerBase
    {
        private readonly ILogger<MattCController> _logger;
        private readonly FinalProjectContext _context;

        public MattCController(ILogger<MattCController> logger, FinalProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.TheMattsTable.ToList());
        }
    }
}
