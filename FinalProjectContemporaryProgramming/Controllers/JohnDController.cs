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
    public class JohnDController : ControllerBase
    {
        
        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<JohnTable> Get() => DBContext.Context.JohnTable;
        public ActionResult Get([FromQuery] int id)
        {
            if (IdExists(id))
            {
                return Ok(GetJohnById(id));
            }
            else 
            {
                return StatusCode(StatusCodes.Status404NotFound, NotFoundMessage);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Post([FromQuery] string FirstName, [FromQuery] string LastName, [FromQuery] string FavoriteSport, [FromQuery] string FavoriteBoardGame, [FromQuery] string FavoriteVideoGame, [FromQuery] string FavoriteTVShow)
        {
            var added = new JohnTable() { Id = GetNextAvailableID(), FirstName = FirstName, LastName = LastName, FavoriteSport = FavoriteSport, FavoriteVideoGame = FavoriteVideoGame, FavoriteTvshow = FavoriteTVShow };
            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(StatusCodes.Status202Accepted, added);
        }

        [HttpPatch]
        [Route("ByID")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName = null, [FromQuery] string LastName = null, [FromQuery] string FavoriteSport = null, [FromQuery] string FavoriteBoardGame = null, [FromQuery] string FavoriteVideoGame = null, [FromQuery] string FavoriteTVShow = null)
        {
            if (!IdExists(id))
                return StatusCode(404,NotFoundMessage);
            Debug.WriteLine("Update by id idk man");
            
            var j = GetJohnById(id);
            
            if (FirstName != null)
                j.FirstName = FirstName;
            if (LastName != null)
                j.LastName = LastName;
            if (FavoriteSport != null)
                j.FavoriteSport = FavoriteSport;
            if (FavoriteVideoGame != null)
                j.FavoriteVideoGame = FavoriteVideoGame;
            if (FavoriteTVShow != null)
                j.FavoriteTvshow = FavoriteTVShow;
            DBContext.Context.JohnTable.Update(j);
            DBContext.Context.SaveChanges();
            return StatusCode(202, j);
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
        public ActionResult Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.JohnTable.Remove(GetJohnById(id));
                DBContext.Context.SaveChanges();
                return Ok(new CustomResponse() {Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted"});
                DBContext.Context.SaveChanges();
            }
            return StatusCode(404, NotFoundMessage);
        }

        private bool IdExists(int id) => DBContext.Context.JohnTable.Any(e => e.Id == id);
        private JohnTable GetJohnById(int id) => DBContext.Context.JohnTable.First(e => e.Id == id);
    }
}
