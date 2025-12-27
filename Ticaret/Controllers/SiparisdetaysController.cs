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
    public class SiparisdetaysController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        // GET: Siparisdetays
        public ActionResult Index()
        {
            var siparisdetay = db.Siparisdetay.Include(s => s.Siparisler).Include(s => s.Urunler);
            return View(siparisdetay.ToList());
        }

        // GET: Siparisdetays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisdetay siparisdetay = db.Siparisdetay.Find(id);
            if (siparisdetay == null)
            {
                return HttpNotFound();
            }
            return View(siparisdetay);
        }

        // GET: Siparisdetays/Create
        public ActionResult Create()
        {
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum");
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi");
            return View();
        }

        // POST: Siparisdetays/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "siparis_detay_id,urun_id,siparis_id,fiyat,adet,toplam_fiyat")] Siparisdetay siparisdetay)
        {
            if (ModelState.IsValid)
            {
                db.Siparisdetay.Add(siparisdetay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", siparisdetay.siparis_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", siparisdetay.urun_id);
            return View(siparisdetay);
        }

        // GET: Siparisdetays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisdetay siparisdetay = db.Siparisdetay.Find(id);
            if (siparisdetay == null)
            {
                return HttpNotFound();
            }
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", siparisdetay.siparis_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", siparisdetay.urun_id);
            return View(siparisdetay);
        }

        // POST: Siparisdetays/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "siparis_detay_id,urun_id,siparis_id,fiyat,adet,toplam_fiyat")] Siparisdetay siparisdetay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siparisdetay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.siparis_id = new SelectList(db.Siparisler, "siparis_id", "durum", siparisdetay.siparis_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", siparisdetay.urun_id);
            return View(siparisdetay);
        }

        // GET: Siparisdetays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisdetay siparisdetay = db.Siparisdetay.Find(id);
            if (siparisdetay == null)
            {
                return HttpNotFound();
            }
            return View(siparisdetay);
        }

        // POST: Siparisdetays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siparisdetay siparisdetay = db.Siparisdetay.Find(id);
            db.Siparisdetay.Remove(siparisdetay);
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
