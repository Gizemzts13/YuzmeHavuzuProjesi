
using HavuzProjesi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HavuzProjesi.Controllers
{
    public class YuzmeController : Controller
    {
        u9747828_havuzEntities db = new u9747828_havuzEntities();
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Yuzme.Include("Kategori").ToList().OrderByDescending(x => x.YuzmeId));
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Yuzme yüzme, HttpPostedFileBase ResimURL)
        {

            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string yuzmeimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Yüzme/" + yuzmeimgname);

                yüzme.ResimURL = "/Uploads/Yüzme/" + yuzmeimgname;
            }
            db.Yuzme.Add(yüzme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Yuzme.Where(x => x.YuzmeId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", b.KategoriId);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(int id, Yuzme yüzme, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Yuzme.Where(x => x.YuzmeId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string yuzmeimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Yüzme/" + yuzmeimgname);

                    b.ResimURL = "/Uploads/Yüzme/" + yuzmeimgname;
                }
                b.Baslik = yüzme.Baslik;
                b.Icerik = yüzme.Icerik;
                b.KategoriId = yüzme.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(yüzme);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yuzme yüzme = db.Yuzme.Find(id);
            if (yüzme == null)
            {
                return HttpNotFound();
            }
            return View(yüzme);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yuzme yüzme = db.Yuzme.Find(id);
            db.Yuzme.Remove(yüzme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}