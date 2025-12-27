using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticaret.Models;

namespace Ticaret.Controllers
{
    public class OdemelerController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        // GET: Odemeler
        public ActionResult Index()
        {
            var odemeler = db.Odemeler.Include(o => o.Siparisler1);
            return View(odemeler.ToList());
        }

        // GET: Odemeler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odemeler odemeler = db.Odemeler.Find(id);
            if (odemeler == null)
            {
                return HttpNotFound();
            }
            return View(odemeler);
        }

        // GET: Odemeler/Create
        public ActionResult Create()
        {
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum");
            return View();
        }

        // POST: Odemeler/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "odeme_id,odeme_tipi,siparis_id")] Odemeler odemeler)
        {
            if (ModelState.IsValid)
            {
                db.Odemeler.Add(odemeler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", odemeler.siparis_id);
            return View(odemeler);
        }

        // GET: Odemeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odemeler odemeler = db.Odemeler.Find(id);
            if (odemeler == null)
            {
                return HttpNotFound();
            }
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", odemeler.siparis_id);
            return View(odemeler);
        }

        // POST: Odemeler/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "odeme_id,odeme_tipi,siparis_id")] Odemeler odemeler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(odemeler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", odemeler.siparis_id);
            return View(odemeler);
        }

        // GET: Odemeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odemeler odemeler = db.Odemeler.Find(id);
            if (odemeler == null)
            {
                return HttpNotFound();
            }
            return View(odemeler);
        }

        // POST: Odemeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Odemeler odemeler = db.Odemeler.Find(id);
            db.Odemeler.Remove(odemeler);
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
