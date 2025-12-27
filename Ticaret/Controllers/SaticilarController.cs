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
    public class SaticilarController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        // GET: Saticilar
        public ActionResult Index()
        {
            var saticilar = db.Saticilar.Include(s => s.Musteriler);
            return View(saticilar.ToList());
        }

        // GET: Saticilar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saticilar saticilar = db.Saticilar.Find(id);
            if (saticilar == null)
            {
                return HttpNotFound();
            }
            return View(saticilar);
        }

        // GET: Saticilar/Create
        public ActionResult Create()
        {
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad");
            return View();
        }

        // POST: Saticilar/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "satici_id,sirket_adi,satici_ad_soyad,telefon,email,adres,ulke,sehir,ilce,musteri_id")] Saticilar saticilar)
        {
            if (ModelState.IsValid)
            {
                db.Saticilar.Add(saticilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", saticilar.musteri_id);
            return View(saticilar);
        }

        // GET: Saticilar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saticilar saticilar = db.Saticilar.Find(id);
            if (saticilar == null)
            {
                return HttpNotFound();
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", saticilar.musteri_id);
            return View(saticilar);
        }

        // POST: Saticilar/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "satici_id,sirket_adi,satici_ad_soyad,telefon,email,adres,ulke,sehir,ilce,musteri_id")] Saticilar saticilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saticilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", saticilar.musteri_id);
            return View(saticilar);
        }

        // GET: Saticilar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saticilar saticilar = db.Saticilar.Find(id);
            if (saticilar == null)
            {
                return HttpNotFound();
            }
            return View(saticilar);
        }

        // POST: Saticilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saticilar saticilar = db.Saticilar.Find(id);
            db.Saticilar.Remove(saticilar);
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
