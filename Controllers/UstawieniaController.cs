using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Childrens_Toy_Store.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Security;

namespace Childrens_Toy_Store.Controllers
{
    public class UstawieniaController : Controller
    {
        // GET: Ustawienia
        
        private Entities db = new Entities();
        private ruleuserwejscie dbrule = new ruleuserwejscie();
        [Authorize(Roles = "admin,normal,moderator")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin,normal,moderator")]
        public ActionResult Usunswojekonto()
        {
            return View();
        }
        
        [Authorize(Roles ="admin,normal,moderator")]
        public ActionResult chcebykontodead()
        {
            List<AspNetUsers> ij = db.AspNetUsers.ToListAsync().Result;
            string idnowe = User.Identity.GetUserName();;
            
            foreach (var item in ij)
            {
                if (item.Email == idnowe)
                {
                    idnowe = item.Id;
                }
            }
            AspNetUsers aspNetUsers =db.AspNetUsers.Find(idnowe);
            Klienci klienci = db.Klienci.Find(idnowe);

            db.Klienci.Remove(klienci);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            FormsAuthentication.SignOut();
            
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [Authorize(Roles = "admin,normal,moderator")]
        public ActionResult produktycrude()
        {
           return RedirectToAction("index", "Produkties");

        }
        [Authorize(Roles = "admin,normal,moderator")]
        public async Task<ActionResult> Edityoudate()
        {
            
            Klienci klienci = await db.Klienci.FindAsync(User.Identity.GetUserId());
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Kliencis/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,normal,moderator")]
        public async Task<ActionResult> Edityoudate([Bind(Include = "Id_uzytkownik,imie,email,nazwisko,ulica,numer_domu,kod_pocztowy,poczta")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(klienci);
        }

        [Authorize(Roles = "admin,moderator")]
        public ActionResult klienciapdate()
        {
            return RedirectToAction("index", "Kliencis");
        }
         
        [Authorize(Roles = "admin,moderator")]
        public ActionResult nadajuprawnienia()
        {
            List<Klienci> nowe = db.Klienci.ToList();
            List<Uprawnieniawyswietl> nowesum = new List<Uprawnieniawyswietl>();
            List<AspNetUserRoles> nih = dbrule.AspNetUserRoles.ToListAsync().Result;
            Uprawnieniawyswietl jij = new Uprawnieniawyswietl();
            for(int i=0; i < nowe.Count(); i++) 
            {
                jij.Id_uzytkownik = nowe[i].Id_uzytkownik;
                jij.imie = nowe[i].imie;
                jij.email = nowe[i].email;
                jij.poczta = nowe[i].poczta;
                for (int ju = 1; ju < nih.Count(); ju++)
                {
                    if (jij.Id_uzytkownik == nih[ju].UserId)
                    {
                        jij.RoleId = nih[ju].RoleId;
                    }
                }
                
                nowesum.Add(jij);
                jij = new Uprawnieniawyswietl();
            }        
            
            return View(nowesum);
        }
        [Authorize(Roles = "admin,moderator")]
        public ActionResult ton(string UserId)
        {
            dbrule.AspNetUserRoles.Where(a => a.UserId == UserId).First().RoleId = "3";
            dbrule.SaveChanges();
            return RedirectToAction("nadajuprawnienia", "Ustawienia");
        }
        [Authorize(Roles = "admin")]
        public ActionResult tom(string UserId)
        {
            dbrule.AspNetUserRoles.Where(a => a.UserId == UserId).First().RoleId="2";
            dbrule.SaveChanges();
            return RedirectToAction("nadajuprawnienia", "Ustawienia");
        }
        [Authorize(Roles = "admin,moderator")]
        public ActionResult ban(string UserId)
        {
            dbrule.AspNetUserRoles.Where(a => a.UserId == UserId).First().RoleId = "4";
            dbrule.SaveChanges();
            return RedirectToAction("nadajuprawnienia", "Ustawienia");
        }
        [Authorize(Roles = "admin,moderator")]
        public ActionResult komenczygood()
        {
            return RedirectToAction("index", "Komentarzes");
        }







        //Tuple jest konniec kontrolera
    }
}
