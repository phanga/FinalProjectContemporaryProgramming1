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
        public class John
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FavoriteSport { get; set; }
            public string FavoriteBoardGame { get; set; }
            public string FavoriteVideoGame { get; set; }
            public string FavoriteTVShow { get; set; }

            public John(JohnTable j)
            {
                ID = j.Id;
                FirstName = j.FirstName;
                LastName = j.LastName;
                FavoriteSport = j.FavoriteSport;
                FavoriteVideoGame = j.FavoriteVideoGame;
                FavoriteTVShow = j.FavoriteTvshow;
            }

            public John()
            {

            }

        }
        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        public John TheBestJohn = new John();
        [HttpGet]
        [Route("All")]

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

        public ActionResult Update([FromQuery] int id, [FromQuery] string Firstname = null, [FromQuery] string LastName = null, [FromQuery] string FavoriteSport = null, [FromQuery] string FavoriteBoardGame = null, [FromQuery] string FavoriteVideoGame = null, [FromQuery] string FavoriteTVShow = null)
        {
            if (!IdExists(id))
                return StatusCode(404,NotFoundMessage);
            Debug.WriteLine("Update by id idk man");
            return StatusCode(202, new {id,Firstname,LastName,FavoriteSport,FavoriteBoardGame,FavoriteVideoGame,FavoriteTVShow});
        }

        private int GetNextAvailableID(){
            int i = 0;
            while (IdExists(i))
                i++;
            return i;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<John> Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.JohnTable.Remove(GetJohnById(id));
                return Ok(new CustomResponse() {Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted"});
            }
            return StatusCode(404, NotFoundMessage);
        }

        private bool IdExists(int id) => DBContext.Context.JohnTable.Any(e => e.Id == id);
        private JohnTable GetJohnById(int id) => DBContext.Context.JohnTable.First(e => e.Id == id);
    }
}
