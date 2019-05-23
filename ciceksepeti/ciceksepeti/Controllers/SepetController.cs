using ciceksepeti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ciceksepeti.Controllers
{
    public class SepetController : Controller
    {
        ciceksepetiEntities entities = new ciceksepetiEntities();

        public ActionResult SepeteEkle(sepeturunliste spt)

        {
            List<sepeturunliste> liste = null;
            sepet sepet = new sepet();

            if (Session["cart"]==null)
            {
                liste = new List<sepeturunliste>();
            }
            else
            {
                liste = (List<sepeturunliste>)Session["cart"];

            }
            
            liste.Add(spt);
            if(spt.urunler==null)
                spt.urunler = entities.urunler.FirstOrDefault(x => x.urun_id == spt.urun_id);
                Session["cart"] = liste;
            return Json("true");

        }

        public ActionResult SepetListe()
        {

            return View((List<sepeturunliste>)Session["cart"]);

        }

        //public ActionResult SepetUrunSil(sepet spt)
        //{
        //    List<sepeturunliste> li = (List<sepeturunliste>)Session["cart"];
            
        //    li.RemoveAll(x => x.sepet_id == spt.sepet_id);
        //    Session["cart"] = li;


        //    return RedirectToAction("SepetListe", "Sepet");
        //}


        public ActionResult Satis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Satis(Satis sts)
        {
           
                entities.Satis.Add(sts);
                entities.SaveChanges();

                List<sepeturunliste> productList = (List<sepeturunliste>)Session["cart"];
                sepet sepet = new sepet();
               
                sepet.kullanıcı_id = int.Parse((string)Session["UserID"]);

                
                sepet.satis_id = sts.satis_id;
                entities.sepet.Add(sepet);
                entities.SaveChanges();

                foreach (var product in productList)
                {
                    product.sepet_id = sepet.sepet_id;
                    product.urunler = null;
                    entities.urunler.FirstOrDefault(x =>x.urun_id == product.urun_id).urun_stok -= product.adet;
                }

                entities.SaveChanges();
            Session["sum"] = null;
            Session["totalcount"] = null;
            Session["cart"] = null;
            Response.Redirect("/Home/Anasayfa");
            return View("Anasayfa");
        }






    }
}