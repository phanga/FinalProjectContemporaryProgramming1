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

        private object NotFoundMessage = new
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };
        private ObjectResult CheckForLies(string FavoriteTA, string FavoriteTeacher, String FavoriteClass)
        {
            if (FavoriteTA != null && FavoriteTA != "Monish Chamlagai")
            {
                return StatusCode(406, new { Title = "Invalid Favorite TA", Message = $"Their Favorite TA clearly isn't '{FavoriteTA}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (FavoriteTeacher != null && FavoriteTeacher != "Awais Shoaib")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Teacher", Message = $"Their Favorite Teacher clearly isn't '{FavoriteTeacher}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (FavoriteClass != null && FavoriteClass != "Contemporary Programming")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Class", Message = $"Their Favorite Class clearly isn't '{FavoriteClass}'. if you wish to fill this table with lies then please mark this person as a liar. " });
            }
            return Ok(null);
        }

      //  [HttpGet]
     //   [Route("All")]
        //private DbSet<NicksTable> aGet() => DBContext.Context.NickTable;
       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get([FromQuery] int? id)
        {
            if(id.HasValue && id.Value != 0)
            {
                return IdExists(id.Value) ? Ok(GetNickById(id.Value)) : StatusCode(404, NotFoundMessage);
            }
            return Ok(DBContext.Context.NickTable.Take(5));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Post([FromQuery] string FirstName, [FromQuery] string LastName, [FromQuery] string FavoriteTA = "Monish Chamlagai", [FromQuery] string FavoriteTeacher = "Awais Shoaib", [FromQuery] string FavoriteClass = "Contemporary Programming", [FromQuery] bool IsALiar = false)
        {
            if (!IsALiar)
            {
                var islying = CheckForLies(FavoriteTA, FavoriteTeacher, FavoriteClass);
                if (islying.StatusCode == 406)
                    return islying;
            }
            var added = new NicksTable() { Id = GetNextAvailableID(), FirstName = FirstName, LastName = LastName, FavoriteTa = FavoriteTA, FavoriteTeacher = FavoriteTeacher, FavoriteClass = FavoriteClass, IAmALiar = IsALiar };
            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(202, added);
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName = null, [FromQuery] string LastName = null, [FromQuery] string FavoriteTA = null, [FromQuery] string FavoriteTeacher = null, [FromQuery] string FavoriteClass = null, [FromQuery] bool? IsALiar = null)
        {
            if (!IdExists(id))
                return StatusCode(404, NotFoundMessage);
            var n = GetNickById(id);
            var islying = CheckForLies(FavoriteTA, FavoriteTeacher, FavoriteClass);
            if (IsALiar.HasValue)
            {
                if (!IsALiar.Value)
                {
                    if (islying.StatusCode == 406)
                        return islying;
                }
            }
            else if (!n.IAmALiar && islying.StatusCode == 406)
            {
                return islying;
            }
            if (FirstName != null)
                n.FirstName = FirstName;
            if (LastName != null)
                n.LastName = LastName;
            if (FavoriteClass != null)
                n.FavoriteClass = FavoriteClass;
            if (FavoriteTeacher != null)
                n.FavoriteTeacher = FavoriteTeacher;
            if (FavoriteTA != null)
                n.FavoriteTa = FavoriteTA;
            if (IsALiar.HasValue)
                n.IAmALiar = IsALiar.Value;
            DBContext.Context.NickTable.Update(n);
            DBContext.Context.SaveChanges();
            return StatusCode(202, n);
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
                DBContext.Context.NickTable.Remove(GetNickById(id));
                DBContext.Context.SaveChanges();
                return Ok(new { Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted." });
            }
            return StatusCode(404, NotFoundMessage);
        }
        private bool IdExists(int id) => DBContext.Context.NickTable.Any(e => e.Id.Equals(id));
        private NicksTable GetNickById(int id) => DBContext.Context.NickTable.First(e => e.Id == id);
    }
}
