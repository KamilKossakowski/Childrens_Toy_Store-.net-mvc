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
    public class KliencisController : Controller
    {
        private Entities db = new Entities();

        // GET: Kliencis
        [Authorize(Roles = "admin,moderator")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Klienci.ToListAsync());
        }

        // GET: Kliencis/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = await db.Klienci.FindAsync(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }



        // GET: Kliencis/Edit/5
        [Authorize(Roles = "admin,moderator,normal")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klienci klienci = await db.Klienci.FindAsync(id);
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
        [Authorize(Roles = "admin,moderator,normal")]
        public async Task<ActionResult> Edit([Bind(Include = "Id_uzytkownik,imie,email,nazwisko,ulica,numer_domu,kod_pocztowy,poczta")] Klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(klienci);
        }

        // GET: Kliencis/Delete/5
     
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
