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
    public class MusterilersController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        // GET: Musterilers
       public ActionResult Index()
        {
            return View(db.Musteriler.ToList());
        }

        // GET: Musterilers/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteriler musteriler = db.Musteriler.Find(id);
            if (musteriler == null)
            {
                return HttpNotFound();
            }
            return View(musteriler);
        }

        // GET: Musterilers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musterilers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "musteri_id,ad_soyad,kullanici_adi,sifre,email,telefon,adres,ulke,sehir,ilce")] Musteriler musteriler)
        {
            if (ModelState.IsValid)
            {
                db.Musteriler.Add(musteriler);
                db.SaveChanges();
				TempData["SuccessMessage"] = "Üye olma başarılı!" +
                    "Şimdi Giriş Yapınız.";
				return RedirectToAction("Hesap", "Home");
			}

            return View(musteriler);
        }

        // GET: Musterilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteriler musteriler = db.Musteriler.Find(id);
            if (musteriler == null)
            {
                return HttpNotFound();
            }
            return View(musteriler);
        }

        // POST: Musterilers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "musteri_id,ad_soyad,kullanici_adi,sifre,email,telefon,adres,ulke,sehir,ilce")] Musteriler musteriler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musteriler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hesabım","Home");
            }
            return View(musteriler);
        }

		// GET: admin
		public ActionResult Edit2(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Musteriler musteriler = db.Musteriler.Find(id);
			if (musteriler == null)
			{
				return HttpNotFound();
			}
			return View(musteriler);
		}

		// POST: admin
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit2([Bind(Include = "musteri_id,ad_soyad,kullanici_adi,sifre,email,telefon,adres,ulke,sehir,ilce")] Musteriler musteriler)
		{
			if (ModelState.IsValid)
			{
				db.Entry(musteriler).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index", "Musterilers");
			}
			return View(musteriler);
		}
		public ActionResult Edit1(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Musteriler musteriler = db.Musteriler.Find(id);
			if (musteriler == null)
			{
				return HttpNotFound();
			}
			return View(musteriler);
		}

		// POST: Musterilers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit1([Bind(Include = "musteri_id,ad_soyad,kullanici_adi,sifre,email,telefon,adres,ulke,sehir,ilce")] Musteriler musteriler)
		{
			if (ModelState.IsValid)
			{
				db.Entry(musteriler).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Adres", "Sepet");
			}
			return View(musteriler);
		}
		// GET: Musterilers/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteriler musteriler = db.Musteriler.Find(id);
            if (musteriler == null)
            {
                return HttpNotFound();
            }
            return View(musteriler);
        }

        // POST: Musterilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musteriler musteriler = db.Musteriler.Find(id);
            db.Musteriler.Remove(musteriler);
            db.SaveChanges();
            return RedirectToAction("Hesap","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //admin
		public ActionResult Details1(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Musteriler musteriler = db.Musteriler.Find(id);
			if (musteriler == null)
			{
				return HttpNotFound();
			}
			return View(musteriler);
		}

		//admin
		public ActionResult Delete1(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Musteriler musteriler = db.Musteriler.Find(id);
			if (musteriler == null)
			{
				return HttpNotFound();
			}
			return View(musteriler);
		}

		// //admin
		[HttpPost, ActionName("Delete1")]
		[ValidateAntiForgeryToken]
		public ActionResult Delete1Confirmed(int id)
		{
			Musteriler musteriler = db.Musteriler.Find(id);
			db.Musteriler.Remove(musteriler);
			db.SaveChanges();
			return RedirectToAction("Index", "Musterilers");
		}
	}
}
