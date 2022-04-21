using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectContemporaryProgramming.Controllers
{
    public class NickBController : Controller
    {
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
