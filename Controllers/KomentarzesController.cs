using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Childrens_Toy_Store.Models;

namespace Childrens_Toy_Store.Controllers
{
    public class KomentarzesController : Controller
    {
        private ruleuserwejscie dbrule = new ruleuserwejscie();
        private Entities db = new Entities();
        int idoiu;

        // GET: Komentarzes
        public async Task<ActionResult> Index(int id=0)
        {
            List<Komentarze> kom = new List<Komentarze>();
            if (id==0)
            {
                return View(await db.Komentarze.OrderByDescending(a=>a.Id_koment).ToListAsync());
            }
            else
            {
                for(int i=0;i<db.Komentarze.ToListAsync().Result.Count(); i++)
                {
                   if(db.Komentarze.ToListAsync().Result[i].id_produktu == id)
                    {
                        kom.Add(db.Komentarze.ToListAsync().Result[i]);
                    }
                }

                return View(kom);
            }
            
        }

        // GET: Komentarzes/Details/5
       

        // GET: Komentarzes/Create
        [Authorize(Roles = "admin,moderator,normal")]
        public ActionResult Create(int id)
        {
            Komentarze komentarze =new Komentarze();
            komentarze.id_produktu = id;
            return View(komentarze);
        }

        // POST: Komentarzes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin,moderator,normal")]
        public async Task<ActionResult> Createaut (Komentarze komentarze)
        {
            if (ModelState.IsValid)
            {
                komentarze.data_dodania = DateTime.Now;
                komentarze.e_mail = User.Identity.Name;
                List<Klienci> hy = db.Klienci.ToList();
                for(int i=0;i<hy.Count();i++)
                {
                    if (User.Identity.Name == hy[i].email)
                        komentarze.id_uzytkownika = hy[i].Id_uzytkownik;
                }
                
                db.Komentarze.Add(komentarze);
                await db.SaveChangesAsync();
                
            }

            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "admin,moderator,normal")]
        public async Task<ActionResult> Delete(int id=0)
        {
            
            Komentarze komentarze = await db.Komentarze.FindAsync(id);
            if ((!User.IsInRole("ban")) && (!User.IsInRole("normal")) && (!(User.Identity.Name != komentarze.e_mail)))
            {
                db.Komentarze.Remove(komentarze);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult ban(string UserId)
        {
            
            dbrule.AspNetUserRoles.FindAsync(db.Komentarze.Find(UserId).id_uzytkownika).Result.RoleId = "4";
            dbrule.SaveChanges();
            return RedirectToAction("nadajuprawnienia", "Ustawienia");
        }
    }
}
