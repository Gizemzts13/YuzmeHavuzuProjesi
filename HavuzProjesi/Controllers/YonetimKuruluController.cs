
using HavuzProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HavuzProjesi.Controllers
{
    public class YonetimKuruluController : Controller
    {

        u9747828_havuzEntities db = new u9747828_havuzEntities();

        public ActionResult Index()
        {
            var h = db.YonetimKurulu.ToList();
            return View(h);
        }

        public ActionResult Edit(int id)
        {
            var h = db.YonetimKurulu.Where(x => x.YonetimKuruluId == id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, YonetimKurulu h)
        {
            if (ModelState.IsValid)
            {
                var yonetimKurulu = db.YonetimKurulu.Where(x => x.YonetimKuruluId == id).SingleOrDefault();

                yonetimKurulu.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }
    }
}