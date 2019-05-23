using ciceksepeti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ciceksepeti.Controllers
{
    public class HomeController : Controller
    {
        ciceksepetiEntities entities = new ciceksepetiEntities();


        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (ciceksepetiEntities entities = new ciceksepetiEntities())
                {
                    var kul = entities.kullanici.Single(x => x.kullanici_email == kullanici.kullanici_email && x.kullanici_sifre == kullanici.kullanici_sifre);
                    if (kul != null)
                    {
                        Session["UserID"] = kul.kullanici_id.ToString();
                        Session["UserAd"] = kul.kullanici_ad.ToString();
                        return RedirectToAction("Anasayfa");
                    }
                }

            }

            return View("UserLogin");
        }

        public ActionResult UserKayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserKayit(kullanici kullanici)
        {
            entities.kullanici.Add(kullanici);
            entities.SaveChanges();
            Response.Redirect("/Home/Anasayfa");
            return View();
        }

        public ActionResult UserLogout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Redirect("~/Home/Anasayfa");
            return View();
        }


        public ActionResult SepetButon()
        {
            return View();
        }



        public ActionResult Anasayfa()
        {
            return View();
        }

        public ActionResult Iletisim()
        {

            return View();
        }

        public PartialViewResult Urunler()
        {
            return PartialView(entities.urunler.ToList());

        }

        public PartialViewResult KategoriUrunListe(int kategoriID)
        {
            return PartialView(entities.urunler.Where(x => x.kategori_id == kategoriID).ToList());
        }

        public ActionResult UrunDetay(int urunId)
        {
            urunler urun = entities.urunler.FirstOrDefault(x=>x.urun_id == urunId);
            return View(urun);
        }
        public PartialViewResult Kategoriler()
        {
            return PartialView(entities.kategoriler.ToList());
        }
    }
}