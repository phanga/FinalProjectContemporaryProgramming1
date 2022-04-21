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
        /*
        // GET: NickBController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NickBController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        


        // GET: NickBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NickBController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NickBController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NickBController/Edit/5
        [HttpPatch]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NickBController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NickBController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
