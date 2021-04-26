
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HavuzProjesi.Models;

namespace HavuzProjesi.Controllers
{
    public class HomeController : Controller
    {
        private u9747828_havuzEntities db = new u9747828_havuzEntities();

        //[Route("")]
        //[Route("Anasayfa")]
        public ActionResult Index()

        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Brans = db.Branslar.ToList().OrderByDescending(x => x.BranslarId);
            ViewBag.Faaliyet = db.Faaliyetler.ToList().OrderByDescending(x => x.FaaliyetlerId);

            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.SliderId));
        }
        public ActionResult BransPartial()
        {
            return View(db.Branslar.ToList());
        }
        public ActionResult FaaliyetPartial()
        {
            return View(db.Faaliyetler.ToList());

        }
        //[Route("YonetimKurulumuz")]
        public ActionResult YonetimKurulu()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.YonetimKurulu.SingleOrDefault());
        }
        //[Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
        //[Route("Branslar")]
        public ActionResult Branslar()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Branslar.ToList().OrderByDescending(x => x.BranslarId));
        }
        //[Route("Duyurular")]
        public ActionResult Duyurular()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Duyurular.SingleOrDefault());
        }
        //[Route("Fiyatlarimiz")]
        public ActionResult Fiyatlar()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Fiyatlar.SingleOrDefault());
        }

        //[Route("iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "uludagolimpik@gmail.com";
                WebMail.Password = "22225555";
                WebMail.SmtpPort = 587;
                WebMail.Send("uludagolimpik@gmail.com", konu, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/iletisim");

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }
        [Route("YuzmeAdi")]
        public ActionResult Yuzme(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Yuzme.Include("Kategori").OrderByDescending(x => x.YuzmeId).ToPagedList(Sayfa, 5));

        }

        public ActionResult YuzmeKategoriPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Kategori.Include("Yuzmes").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult YuzmeKayitPartial()
        {

            return PartialView(db.Yuzme.ToList().OrderByDescending(x => x.YuzmeId));
        }
        [Route("YuzmeAdi/{kategoriad}/{id:int}")]
        public ActionResult KategoriYuzme(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Yuzme.Include("Kategori").OrderByDescending(x => x.YuzmeId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(b);
        }
        [Route("YuzmeAdi/{baslik}-{id:int}")]
        public ActionResult YuzmeDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Yuzme.Include("Kategori").Where(x => x.YuzmeId == id).SingleOrDefault();
            return View(b);
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            ViewBag.Brans = db.Branslar.ToList().OrderByDescending(x => x.BranslarId);

            ViewBag.Yuzme = db.Yuzme.ToList().OrderByDescending(x => x.YuzmeId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            return PartialView();

        }

    }
}
