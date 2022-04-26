using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectContemporaryProgramming.Models.DataLayer;

namespace FinalProjectContemporaryProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainTableController : ControllerBase
    {
        public class Main : Gabe
        {
        }


        [HttpGet]
        [Route("All")]
        public IEnumerable<Main> Get()
        {
            List<Main> ret = new List<Main>();
            for(int i = 0; i < 5; i++ )
            {
                ret.Add(new Main()  { ID = i, FullName = "Gabriel Phan" }) ;
            }
            return ret;
        }
        [HttpGet]
        [Route("MainID")]
        public Main Get([FromQuery] int id)
        {
            Debug.WriteLine("Cheerio m8");
            return new Main() { ID = id, FullName = "Gabriel Phan" };
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        public ActionResult Post([FromQuery] string FullName, [FromQuery] DateTime Birthdate, [FromQuery] string CollegeProgram, [FromQuery] string YearInProgram)
        {
           return StatusCode(202, new Main());
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Main> Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                return Ok(new Main() { ID = id });
            }
            return StatusCode(404, new { id });
        }
        private bool IdExists(int id)
        {
            return true;
        }
        private Main GetMainById(int id)
        {
            return new Main() { ID = id };
        }

    }
   
}
