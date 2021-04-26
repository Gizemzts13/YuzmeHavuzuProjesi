
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
    public class FaaliyetlerController : Controller
    {
        u9747828_havuzEntities db = new u9747828_havuzEntities();

        public ActionResult Index()
        {
            return View(db.Faaliyetler.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Faaliyetler faaliyetler, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Faaliyetler/" + logoname);

                    faaliyetler.ResimURL = "/Uploads/Faaliyetler/" + logoname;
                }
                db.Faaliyetler.Add(faaliyetler);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(faaliyetler);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Faaliyet Bulunamadı";
            }
            var faaliyetler = db.Faaliyetler.Find(id);
            if (faaliyetler == null)
            {
                return HttpNotFound();
            }

            return View(faaliyetler);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Faaliyetler faaliyetler, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var h = db.Faaliyetler.Where(x => x.FaaliyetlerId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string faaliyetname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Faaliyetler/" + faaliyetname);

                    h.ResimURL = "/Uploads/Faaliyetler/" + faaliyetname;
                }

                h.Baslik = faaliyetler.Baslik;
                h.Aciklama = faaliyetler.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var h = db.Faaliyetler.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Faaliyetler.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}