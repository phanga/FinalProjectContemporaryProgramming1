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

            public string FavoriteTA => "Monish Chamlagai";
            public string FavoriteTeacher => "Awais Shoaib";
            public string FavoriteClass => "Contemporary Programming";
        }
        [HttpGet]
        public IEnumerable<Nick> Get()
        {
            return new List<Nick>() { new Nick() { ID = -1, FirstName = "Nick", LastName = "Bell" } };
        }
        [HttpGet()]
        [Route("Default/NickB{ID=int}")]
        public Nick Get(int id=-2)
        {
            Debug.WriteLine("Im here nick");
            return new Nick() { ID = id, FirstName = "Nick", LastName = "Bell" };
        }
       
    }
}
