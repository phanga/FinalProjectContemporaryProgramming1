using FinalProjectContemporaryProgramming.Models.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace FinalProjectContemporaryProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MattCController : ControllerBase
    {
        public class Matt : MattTable
        {
        }

        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<MattTable> Get()
        {
            return DBContext.Context.MattTable.ToList();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [Route("ByID")]
        public ActionResult Get([FromQuery] int Id)
        {
            if (IdExists(Id))
            {
                return Ok(GetMattById(Id));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, NotFoundMessage);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Post(
            [FromQuery] string FirstName,
            [FromQuery] string LastName,
            [FromQuery] string FavoriteBreakfeast = null,
            [FromQuery] string FavoriteDinner = null,
            [FromQuery] string FavoriteDessert = null
            )
        {
            var posted = new MattTable() { 
                Id = GetNextAvailableID(), 
                FirstName = FirstName, 
                LastName = LastName,
                FavoriteBreakfeast = FavoriteBreakfeast,
                FavoriteDinner = FavoriteDinner,
                FavoriteDessert = FavoriteDessert
            };
            
            DBContext.Context.Add(posted);
            DBContext.Context.SaveChanges();
            return StatusCode(202, posted);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(
            [FromQuery] int Id, 
            [FromQuery] string FirstName = null, 
            [FromQuery] string LastName = null, 
            [FromQuery] string FavoriteBreakfeast = null, 
            [FromQuery] string FavoriteDinner = null,
            [FromQuery] string FavoriteDessert = null)
        {
            if (!IdExists(Id))
                return StatusCode(404, NotFoundMessage);
            var m = GetMattById(Id);
            if (FirstName != null)
                m.FirstName = FirstName;
            if (LastName != null)
                m.LastName = LastName;
            if (FavoriteBreakfeast != null)
                m.FavoriteBreakfeast = FavoriteBreakfeast;
            if (FavoriteDinner != null)
                m.FavoriteDinner = FavoriteDinner;
            if (FavoriteDessert != null)
                m.FavoriteDessert = FavoriteDessert;
            DBContext.Context.MattTable.Update(m);
            DBContext.Context.SaveChanges();
            return StatusCode(202, m);
        }
        private int GetNextAvailableID()
        {
            int i = 1;
            while (IdExists(i))
                i++;
            return i;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Matt> Delete([FromQuery] int Id)
        {
            if (IdExists(Id))
            {
                DBContext.Context.MattTable.Remove(GetMattById(Id));
                DBContext.Context.SaveChanges();
                return Ok(new CustomResponse() { Title = "Successfully Deleted", Message = "Row with ID: " + Id + " has been deleted." });
            }
            return StatusCode(404, new { Id });
        }
        private bool IdExists(int Id) => DBContext.Context.MattTable.Any(e => e.Id.Equals(Id));
        private MattTable GetMattById(int Id)
        {
            return DBContext.Context.MattTable.First(e => e.Id == Id);
        }
    }
}
