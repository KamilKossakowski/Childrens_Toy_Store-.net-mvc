using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Childrens_Toy_Store.Models;

namespace Childrens_Toy_Store.Controllers
{
    public class ProduktiesController : Controller
    {
        private Entities db = new Entities();

        // GET: Produkties
        public ActionResult Index()
        {
            return View(db.Produkty.ToList());
        }

        // GET: Produkties/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            return View(produkty);
        }

        // GET: Produkties/Create
        [Authorize(Roles = "admin,moderator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produkties/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin,moderator")]
        public ActionResult Create( Produktycreateedit produkty)
        {
            Produkty produkty1 = new Produkty();
            string filename = Path.GetFileNameWithoutExtension(produkty.obrazeknazwa.FileName);
            string extension = Path.GetExtension(produkty.obrazeknazwa.FileName);
            filename += DateTime.Now.ToString("yymmssfff") + extension;
            produkty1.obrazek = "/obrazki/" + filename;
            produkty1.nazwa = produkty.nazwa;
            produkty1.cena = produkty.cena;
            produkty1.dozw_wiek = produkty.dozw_wiek;
            produkty1.opis = produkty.opis;
            produkty1.rok_produkcji = produkty.rok_produkcji;
            
            filename = Path.Combine(Server.MapPath("~/obrazki/"), filename);
            produkty.obrazeknazwa.SaveAs(filename);
            if (ModelState.IsValid)
            {
                
                db.Produkty.Add(produkty1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produkty);
        }

        // GET: Produkties/Edit/5
        [Authorize(Roles ="admin,moderator")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            return View(produkty);
        }

        // POST: Produkties/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public ActionResult Edit([Bind(Include = "Id_produktu,nazwa,opis,cena,dozw_wiek,rok_produkcji")] Produkty produkty)
        {
            if (ModelState.IsValid)
            {

                db.Entry(produkty).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produkty);
        }

        // GET: Produkties/Delete/5
        [Authorize(Roles ="admin,moderator")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkty produkty = db.Produkty.Find(id);
            if (produkty == null)
            {
                return HttpNotFound();
            }
            return View(produkty);
        }

        // POST: Produkties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Produkty produkty = db.Produkty.Find(id);
            db.Produkty.Remove(produkty);
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
