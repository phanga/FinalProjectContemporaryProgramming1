using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            public bool IsALiar { get; set; }
        }
        public Nick TheBestNick = new Nick();
        /// <summary>
        /// gets every element in the table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IEnumerable<Nick> Get()
        {
            List<Nick> ret = new List<Nick>();
            for (int i = 0; i < 5; i++)
            {
                ret.Add(new Nick() { ID = i, FirstName = "Nick", LastName = "Bell" });
            }
            return ret;
        }
        [HttpGet]
        [Route("ByID")]
        public Nick Get([FromQuery] int id)
        {
            Debug.WriteLine("Im here nick");
            return new Nick() { ID = id, FirstName = "Nick", LastName = "Bell" };
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
            Debug.WriteLine("Change NickBController.Post to actually post to database");
            //Create new database Row 
            return StatusCode(202,new Nick());
        }
        [HttpPatch]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName=null, [FromQuery] string LastName=null, [FromQuery] string FavoriteTA = null, [FromQuery] string FavoriteTeacher = null, [FromQuery] string FavoriteClass = null, [FromQuery] bool IsALiar = false)
        {
            if (GetNickById(id) == null)
                return StatusCode(404, new { id });
            Debug.WriteLine("Update by id idk man");
            return StatusCode(202, new {id,FirstName, LastName,FavoriteTA});
        }
        private int GetNextAvailableID()
        {
            Debug.WriteLine("Change NickBController.GetNextAvailableID to actually get the next available id");
            return 0;//todo check database for next available id
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Nick> Delete([FromQuery]int id)
        {
            if(IdExists(id))
            {
                //delete id and return it
                return Ok(new Nick() { ID = id });
            }
            return StatusCode(404,new { id });
        }
        private bool IdExists(int id)
        {
            return true;
        }
        private Nick GetNickById(int id)
        {
            //if id exists return nick object, if it doesnt then return null
            return new Nick() { ID = id };
        }
    }
}
