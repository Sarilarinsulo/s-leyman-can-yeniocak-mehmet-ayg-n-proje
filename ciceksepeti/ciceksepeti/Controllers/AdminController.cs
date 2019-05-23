using ciceksepeti.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ciceksepeti.Controllers
{
    public class AdminController : Controller
    {
        ciceksepetiEntities entities = new ciceksepetiEntities();
        public ActionResult Index()
        { 

            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(admin admin)
        {
            if (ModelState.IsValid)
            {
                using (ciceksepetiEntities entities = new ciceksepetiEntities())
                {
                    var usr = entities.admin.Where(x => x.admin_kullaniciad == admin.admin_kullaniciad && x.admin_sifre == admin.admin_sifre).FirstOrDefault();
                    if (usr != null)
                    {
                        Session["AdminID"] = usr.admin_id.ToString();
                        Session["AdminAd"] = usr.admin_ad.ToString();
                        Response.Redirect("/Admin/Index");
                    }
                }
                Session["Hata"] = "Hatalı Kulllanıcı Adı ve ya Şifre!";
            }

            return View("AdminLogin");
        }

        public ActionResult AdminLogout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            
            Response.Redirect("~/Admin/AdminLogin");
            return View();
        }




        public ActionResult Urunler()
        {
            return View(entities.urunler.ToList());
        }

        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = entities.kategoriler.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(urunler urunler)
        {
            entities.urunler.Add(urunler);
            entities.SaveChanges();
            return RedirectToAction("Urunler");

        }


        public ActionResult UrunSil(int urunID)
        {
            urunler urun = entities.urunler.FirstOrDefault(x => x.urun_id == urunID);
            resimler resim = entities.resimler.FirstOrDefault(x => x.urun_id == urunID);
            entities.urunler.Remove(urun);
            if (resim != null)
            {
                entities.resimler.Remove(resim);
            }

            entities.SaveChanges();
            return RedirectToAction("Urunler");
        }


        public ActionResult ResimEkle(int urunID)
        {

            return View(urunID);
        }

        [HttpPost]
        public ActionResult ResimEkle(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap resimBoyut = new Bitmap(img, Ayarlar.Ayarlar.UrunBoyut);
                string uzanti = "/Resimler/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                resimBoyut.Save(Server.MapPath(uzanti));
                resimler resimler = new resimler
                {
                    resim_yol = uzanti,
                    urun_id = uId
                };
                if (entities.resimler.FirstOrDefault(x => x.urun_id == uId && x.resim_bit == false) != null)
                {
                    resimler.resim_bit = true;
                }
                else
                {
                    resimler.resim_bit = false;
                }

                entities.resimler.Add(resimler);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public ActionResult ResimGuncelle(int urunID)
        {

            return View(urunID);
        }

        [HttpPost]
        public ActionResult ResimGuncelle(int uId, HttpPostedFileBase fileUpload)
        {
            resimler resim = entities.resimler.FirstOrDefault(x => x.urun_id == uId);
            if(resim!=null)
            {
                entities.resimler.Remove(resim);
            }
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap resimBoyut = new Bitmap(img, Ayarlar.Ayarlar.UrunBoyut);
                string uzanti = "/Resimler/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                resimBoyut.Save(Server.MapPath(uzanti));
                resimler resimler = new resimler
                {
                    resim_yol = uzanti,
                    urun_id = uId
                };
                if (entities.resimler.FirstOrDefault(x => x.urun_id == uId && x.resim_bit == false) == null)
                {
                    resimler.resim_bit = true;
                }
                else
                {
                    resimler.resim_bit = false;
                }

                entities.resimler.Add(resimler);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public ActionResult Kategoriler()
        {
            return View(entities.kategoriler.ToList());
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(kategoriler kategori)
        {
            entities.kategoriler.Add(kategori);
            entities.SaveChanges();
            return RedirectToAction("Kategoriler");
        }

        public ActionResult KategoriSil(int kategoriID)
        {
            urunler urun = entities.urunler.FirstOrDefault(x => x.kategori_id == kategoriID);
            if (urun == null)
            {
                kategoriler kategori = entities.kategoriler.FirstOrDefault(x => x.kategori_id == kategoriID);
                entities.kategoriler.Remove(kategori);
                entities.SaveChanges();
            }
            else if (urun != null)
            {
                Session["kategorihata"] = "Lütfen Önce Kategoriye Ait Ürünleri Siliniz.";
            }
            return RedirectToAction("Kategoriler");
        }


    }
}