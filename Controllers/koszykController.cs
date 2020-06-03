using Childrens_Toy_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Childrens_Toy_Store.Controllers
{
    public class koszykController : Controller
    {
        private Entities db = new Entities();
        // GET: koszyk
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult wyswietlkoszyk()
        {
            List<Klienci> hy = db.Klienci.ToList();
            string mordet = "";
            for (int i = 0; i < hy.Count(); i++)
            {
                if (User.Identity.Name == hy[i].email)
                    mordet = hy[i].Id_uzytkownik;
            }
            List<CoZamowione> yu = db.CoZamowione.ToList();
            List<CoZamowione> nori = new List<CoZamowione>();
            for (int i = 0; i < yu.Count(); i++)
            {
                if (yu[i].Id_zamowienia == mordet)
                {
                    nori.Add(yu[i]);
                }
            }
            List<Produkty> produkties = new List<Produkty>();
            for (int i = 0; i < nori.Count; i++)
            {
                produkties.Add(db.Produkty.Find(nori[i].Id_produktu));
            }
            return View(produkties);
        }


        // GET: koszyk/Delete/5
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult Delete(int id)
        {
            List<Klienci> hy = db.Klienci.ToList();
            string mordet = "";
            for (int i = 0; i < hy.Count(); i++)
            {
                if (User.Identity.Name == hy[i].email)
                    mordet = hy[i].Id_uzytkownik;
            }
            List<CoZamowione> yu = db.CoZamowione.ToList();
            List<CoZamowione> nori = new List<CoZamowione>();
            for (int i = 0; i < yu.Count(); i++)
            {
                if (yu[i].Id_zamowienia == mordet)
                {
                    nori.Add(yu[i]);
                }
            }
            CoZamowione hi = new CoZamowione();
            for (int i = 0; i < nori.Count(); i++)
            {
                if (nori[i].Id_produktu == id)
                {
                    hi = nori[i];
                }
            }
            db.CoZamowione.Remove(hi);
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult Wyczysckoszyk()
        {
            List<Klienci> hy = db.Klienci.ToList();
            string mordet = "";
            for (int i = 0; i < hy.Count(); i++)
            {
                if (User.Identity.Name == hy[i].email)
                    mordet = hy[i].Id_uzytkownik;
            }
            List<CoZamowione> kji = db.CoZamowione.ToList();
            for (int i = 0; i < kji.Count(); i++)
            {
                if (kji[i].e_mail == User.Identity.Name&&kji[i].Id_zamowienia==mordet)
                {
                    db.CoZamowione.Remove(kji[i]);
                }
                db.SaveChanges();
            }
            return RedirectToAction("index", "Home");

        }
        
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult addprodukt(int id)
        {
            List<Klienci> hy = db.Klienci.ToList();
            string mordet = "";
            for (int i = 0; i < hy.Count(); i++)
            {
                if (User.Identity.Name == hy[i].email)
                    mordet = hy[i].Id_uzytkownik;
            }
            CoZamowione kuj = new CoZamowione();
            kuj.Id_produktu = id;
            kuj.Id_zamowienia = mordet;
            kuj.e_mail = User.Identity.Name;
            db.CoZamowione.Add(kuj);
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }
        ///w panelu ustawienia
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult mojezamowienia()
            {
            
            return View(db.zamowienia.Where(a => a.e_mail == User.Identity.Name).OrderBy(a=>a.status).ToList());
            }
        
        [Authorize(Roles = "admin,moderator")]
        public ActionResult dowyslania()
        {

            return View(db.zamowienia.Where(a => a.status == 1).ToList());
        }
        [Authorize(Roles = "admin,moderator")]
        public ActionResult realizacjazam(string id)
        {
            List<Klienci> hy = db.Klienci.ToList();
            string mordet = "";
            for (int i = 0; i < hy.Count(); i++)
            {
                if (User.Identity.Name == hy[i].email)
                    mordet = hy[i].Id_uzytkownik;
            }
            db.zamowienia.Find(id).id_pracownika = mordet;
            db.zamowienia.Find(id).data_wyslania=DateTime.Now;
            db.zamowienia.Find(id).status = 2;
            db.SaveChanges();
            return RedirectToAction("dowyslania");
        }
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult zluzzamuwienia()
        {
            Klienci userid = db.Klienci.Where(a => a.email == User.Identity.Name).First();
            //List <CoZamowione> kji = db.CoZamowione.ToList();
            //string uyt = DateTime.Now.ToString("dd MMM HH:mm:ss") + User.Identity.Name.Substring(0, 10);
            //Klienci iju = db.Klienci.Find(kji[1].Id_zamowienia);
            //zamowienia ijz = db.zamowienia.Find(kji[1].Id_zamowienia);
            //for (int i = 0; i < kji.Count(); i++)
            //{
            //    if (kji[i].e_mail == User.Identity.Name)
            //    {
            //        db.CoZamowione.Remove(kji[i]);
            //        kji[i].Id_zamowienia = uyt;
            //        db.CoZamowione.Add(kji[i]);
            //    }
            //}

            //ijz.ad_wysylki = iju.imie + " " + iju.nazwisko + " \n" + iju.ulica + " " + iju.numer_domu + "\n" + iju.kod_pocztowy + " " + iju.poczta;


            return View(userid);

        }
        public ActionResult zamawiakont(Klienci klienci)
        {
            string uyt = DateTime.Now.ToString("ddMMMHHmmss") + User.Identity.Name.Substring(0, 10);
            List<CoZamowione> hiu = db.CoZamowione.Where(a => a.Id_zamowienia == klienci.Id_uzytkownik).ToList();
            for(int i = 0; i < hiu.Count(); i++)
            {
                db.CoZamowione.Remove(hiu[i]);
            }
            
            db.SaveChanges();
            for (int i = 0; i < hiu.Count(); i++)
            {
                hiu[i].Id_zamowienia = uyt;
                db.CoZamowione.Add(hiu[i]);
            }
            db.SaveChanges();
            zamowienia z = new zamowienia();
            z.id_klienta = klienci.Id_uzytkownik;
            z.Id_zamowienia = uyt;
            z.e_mail = klienci.email;
            z.status = 1;
            z.ad_wysylki=klienci.imie+" " + klienci.nazwisko + " \n" + klienci.ulica + " " + klienci.numer_domu + "\n" + klienci.kod_pocztowy + " " + klienci.poczta;
            db.zamowienia.Add(z);
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }
        public ActionResult produktyzzam(string id)
        {
            List<Produkty> ju = new List<Produkty>();
            List<CoZamowione> uh = db.CoZamowione.ToList();
            for(int ki = 0; ki < uh.Count(); ki++)
            {
                if (uh[ki].Id_zamowienia == id)
                {
                    ju.Add(db.Produkty.Find(uh[ki].Id_produktu));
                }
            }
            return View(ju);
        }
        
    }
}
