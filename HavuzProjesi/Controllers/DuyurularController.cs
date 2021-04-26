
using HavuzProjesi.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HavuzProjesi.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        u9747828_havuzEntities db = new u9747828_havuzEntities();

        public ActionResult Index()
        {
            var h = db.Duyurular.ToList();
            return View(h);
        }

        public ActionResult Edit(int id)
        {
            var h = db.Duyurular.Where(x => x.DuyurularId == id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Duyurular h)
        {
            if (ModelState.IsValid)
            {
                var duyurular = db.Duyurular.Where(x => x.DuyurularId == id).SingleOrDefault();

                duyurular.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }
    }
}