
using HavuzProjesi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HavuzProjesi.Controllers
{
    public class FiyatlarController : Controller
    {
        u9747828_havuzEntities db = new u9747828_havuzEntities();

        public ActionResult Index()
        {
            var h = db.Fiyatlar.ToList();
            return View(h);
        }

        public ActionResult Edit(int id)
        {
            var h = db.Fiyatlar.Where(x => x.FiyatlarId == id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Fiyatlar h)
        {
            if (ModelState.IsValid)
            {
                var fiyatlar = db.Fiyatlar.Where(x => x.FiyatlarId == id).SingleOrDefault();

                fiyatlar.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }
    }
}