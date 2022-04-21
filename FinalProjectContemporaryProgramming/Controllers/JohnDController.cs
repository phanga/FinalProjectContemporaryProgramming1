using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectContemporaryProgramming.Controllers
{
    public class JohnDController : Controller
    {
        // GET: JohnDController
        public ActionResult Index()
        {
            return View();
        }

        // GET: JohnDController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JohnDController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JohnDController/Create
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

        // GET: JohnDController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JohnDController/Edit/5
        [HttpPost]
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

       
    }
}
