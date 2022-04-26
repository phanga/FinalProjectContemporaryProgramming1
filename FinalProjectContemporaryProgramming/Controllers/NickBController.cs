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
    public class NickBController : ControllerBase
    {
        public class Nick
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string FavoriteTA { get; set; } = "Monish Chamlagai";
            public string FavoriteTeacher { get; set; } = "Awais Shoaib";
            public string FavoriteClass { get; set; }= "Contemporary Programming";
            //public bool IsALiar { get; set; }
            public Nick(NicksTable n)
            {
                ID = n.Id;
                FirstName = n.FirstName;
                LastName = n.LastName;
                FavoriteTA = n.FavoriteTa;
                FavoriteTeacher = n.FavoriteTeacher;
                FavoriteClass = n.FavoriteClass;
            }
            public Nick()
            {

            }
        }
        private CustomResponse NotFoundMessage=new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };
        public Nick TheBestNick = new Nick();
        /// <summary>
        /// gets every element in the table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IEnumerable<Nick> Get()
        {
            return DBContext.Context.NicksTable.Select(e => new Nick(e));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [Route("ByID")]
        public ActionResult Get([FromQuery] int id)
        {
            if (IdExists(id))
            {
                return Ok(GetNickById(id));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound,NotFoundMessage);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Post([FromQuery]string FirstName, [FromQuery] string LastName, [FromQuery] string FavoriteTA= "Monish Chamlagai", [FromQuery] string FavoriteTeacher= "Awais Shoaib", [FromQuery] string FavoriteClass= "Contemporary Programming", [FromQuery] bool IsALiar=false)
        {
            if (!IsALiar)
            {
                if (FavoriteTA != TheBestNick.FavoriteTA)
                {
                    return StatusCode(406, new CustomResponse() { Title = "Invalid Favorite TA", Message = $"Their Favorite TA clearly isn't '{FavoriteTA}'. If you wish to fill this table with lies then please mark this person as a liar. "});
                }else if (FavoriteTeacher != TheBestNick.FavoriteTeacher)
                {
                    return StatusCode(406, new CustomResponse() { Title = "Invalid Favorite Teacher", Message = $"Their Favorite Teacher clearly isn't '{FavoriteTeacher}'. If you wish to fill this table with lies then please mark this person as a liar. " });
                }
                else if(FavoriteClass != TheBestNick.FavoriteClass)
                {
                    return StatusCode(406, new CustomResponse() { Title = "Invalid Favorite Class", Message = $"Their Favorite Class clearly isn't '{FavoriteClass}'. if you wish to fill this table with lies then please mark this person as a liar. " });
                }
            }
            var added = new NicksTable() { Id = GetNextAvailableID(),FirstName=FirstName,LastName=LastName,FavoriteTa=FavoriteTA,FavoriteTeacher=FavoriteTeacher,FavoriteClass=FavoriteClass };
            DBContext.Context.Add(added);
            return StatusCode(202,new Nick(added));
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName=null, [FromQuery] string LastName=null, [FromQuery] string FavoriteTA = null, [FromQuery] string FavoriteTeacher = null, [FromQuery] string FavoriteClass = null, [FromQuery] bool IsALiar = false)
        {
            if (!IdExists(id))
                return StatusCode(404,NotFoundMessage);
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
        public ActionResult<Nick> Delete([FromQuery]int id)
        {
            if(IdExists(id))
            {
                DBContext.Context.NicksTable.Remove(GetNickById(id));
                return Ok(new CustomResponse() {Title="Successfully Deleted",Message="Row with ID: "+id+" has been deleted." });
            }
            return StatusCode(404,NotFoundMessage);
        }
        private bool IdExists(int id) => DBContext.Context.NicksTable.Any(e => e.Id.Equals(id));
        private NicksTable GetNickById(int id)
        {
            return DBContext.Context.NicksTable.First(e => e.Id == id);
        }
    }
}
