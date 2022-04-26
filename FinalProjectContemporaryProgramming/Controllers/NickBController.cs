using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectContemporaryProgramming.Models.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectContemporaryProgramming.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class NickBController : ControllerBase
    {

        private object NotFoundMessage=new
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        [HttpGet]
        [Route("All")]
        public DbSet<NicksTable> Get() => DBContext.Context.NickTable;
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [Route("ByID")]
        public ActionResult Get([FromQuery] int id)=> IdExists(id)? Ok(GetNickById(id)): StatusCode(404, NotFoundMessage);
     
        private ObjectResult CheckForLies(string FavoriteTA, string FavoriteTeacher, String FavoriteClass)
        {
            if (FavoriteTA != "Monish Chamlagai")
            {
                return StatusCode(406, new { Title = "Invalid Favorite TA", Message = $"Their Favorite TA clearly isn't '{FavoriteTA}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (FavoriteTeacher != "Awais Shoaib")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Teacher", Message = $"Their Favorite Teacher clearly isn't '{FavoriteTeacher}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (FavoriteClass != "Contemporary Programming")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Class", Message = $"Their Favorite Class clearly isn't '{FavoriteClass}'. if you wish to fill this table with lies then please mark this person as a liar. " });
            }
            return Ok(null);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Post([FromQuery]string FirstName, [FromQuery] string LastName, [FromQuery] string FavoriteTA= "Monish Chamlagai", [FromQuery] string FavoriteTeacher= "Awais Shoaib", [FromQuery] string FavoriteClass= "Contemporary Programming", [FromQuery] bool IsALiar=false)
        {
            if (!IsALiar)
            {
                var islying =CheckForLies(FavoriteTA, FavoriteTeacher, FavoriteClass);
                if (islying.StatusCode == 406)
                    return islying;
            }
            var added = new NicksTable() { Id = GetNextAvailableID(),FirstName=FirstName,LastName=LastName,FavoriteTa=FavoriteTA,FavoriteTeacher=FavoriteTeacher,FavoriteClass=FavoriteClass,IAmALiar=IsALiar };
            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(202,added);
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName=null, [FromQuery] string LastName=null, [FromQuery] string FavoriteTA = null, [FromQuery] string FavoriteTeacher = null, [FromQuery] string FavoriteClass = null, [FromQuery] bool IsALiar = false)
        {
            if (!IdExists(id))
                return StatusCode(404,NotFoundMessage);
            if (!IsALiar) { 
                var islying = CheckForLies(FavoriteTA, FavoriteTeacher, FavoriteClass);
                if (islying.StatusCode == 406)
                    return islying;
            }
            
            Debug.WriteLine("Update by id idk man");
            return StatusCode(202, new {id,FirstName, LastName,FavoriteTA});
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
        public ActionResult Delete([FromQuery]int id)
        {
            if(IdExists(id))
            {
                Get().Remove(GetNickById(id));
                DBContext.Context.SaveChanges();
                return Ok(new {Title="Successfully Deleted",Message="Row with ID: "+id+" has been deleted." });
            }
            return StatusCode(404,NotFoundMessage);
        }
        private bool IdExists(int id) => Get().Any(e => e.Id.Equals(id));
        private NicksTable GetNickById(int id) =>Get().First(e => e.Id == id);
    }
}
