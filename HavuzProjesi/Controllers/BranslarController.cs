
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
    public class BranşlarController : Controller
    {
        u9747828_havuzEntities db = new u9747828_havuzEntities();
        public ActionResult Index()
        {
            return View(db.Branslar.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Branslar branslar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Hizmet/" + logoname);

                    branslar.ResimURL = "/Uploads/Hizmet/" + logoname;
                }
                db.Branslar.Add(branslar);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(branslar);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var branslar = db.Branslar.Find(id);
            if (branslar == null)
            {
                return HttpNotFound();
            }

            return View(branslar);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Branslar branslar, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var h = db.Branslar.Where(x => x.BranslarId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string bransname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Hizmet/" + bransname);

                    h.ResimURL = "/Uploads/Hizmet/" + bransname;
                }

                h.Baslik = branslar.Baslik;
                h.Aciklama = branslar.Aciklama;
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
            var h = db.Branslar.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Branslar.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}