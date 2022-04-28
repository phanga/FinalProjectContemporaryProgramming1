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
    public class GabePController : ControllerBase
    {
        public class Main : GabeTable
        {
        }

        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<GabeTable> Get()
        {
            return DBContext.Context.GabeTable.ToList();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [Route("ByID")]
        public ActionResult Get([FromQuery] int id)
        {
            if (IdExists(id))
            {
                return Ok(GetMainById(id));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, NotFoundMessage);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        public ActionResult Post(
            [FromQuery] string FullName, 
            [FromQuery] DateTime Birthdate, 
            [FromQuery] string CollegeProgram, 
            [FromQuery] string YearInProgram
            )
        {
            var added = new GabeTable() { ID = GetNextAvailableID(), 
                FullName = FullName, 
                Birthdate = Birthdate, 
                CollegeProgram = CollegeProgram, 
                YearInProgram = YearInProgram };

            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(202, added);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(
            [FromQuery] int id, 
            [FromQuery] string FullName = null, 
            [FromQuery] DateTime? Birthdate = null, 
            [FromQuery] string CollegeProgram = null, 
            [FromQuery] string YearInProgram = null)
        {
            if (!IdExists(id))
                return StatusCode(404, NotFoundMessage);
            return StatusCode(202, new { 
                id, 
                FullName, 
                Birthdate, 
                CollegeProgram,
                YearInProgram
            });
        }
        private int GetNextAvailableID()
        {
            int i = 0;
            while (IdExists(i))
                i++;
            return i;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Main> Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.GabeTable.Remove(GetMainById(id));
                return Ok(new CustomResponse() { Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted." });
            }
            return StatusCode(404, new { id });
        }
        private bool IdExists(int id) => DBContext.Context.GabeTable.Any(e => e.ID.Equals(id));
        private GabeTable GetMainById(int id)
        {
            return DBContext.Context.GabeTable.First(e => e.ID == id);
        }
    }

}