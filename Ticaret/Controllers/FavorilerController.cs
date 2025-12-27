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
    public class FavorilerController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavorites(int urun_id)
        {

            var musteri_id = (int)Session["musteri_id"];

            if (musteri_id == 0)
            {
                TempData["SuccessMessage"] = "Kullanıcı bilgileri alınamadı.";
            }

            var mevcutFavori = db.Favoriler.Any(f => f.musteri_id == musteri_id && f.urun_id == urun_id);

            if (mevcutFavori)
            {
                TempData["ErrorMessage"] = "Bu ürün zaten favorilerinizde.";
                return RedirectToAction("Index1", "Urunler");

            }

            var favori = new Favoriler { musteri_id = musteri_id, urun_id = urun_id };
            db.Favoriler.Add(favori);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Ürün favorilere eklendi.";
            return RedirectToAction("Index1", "Urunler");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddToFavorites1(int urun_id)
		{
			var musteri_id = (int)Session["musteri_id"];

			if (musteri_id == 0)
			{
				TempData["SuccessMessage"] = "Kullanıcı bilgileri alınamadı.";
			}

			var mevcutFavori = db.Favoriler.Any(f => f.musteri_id == musteri_id && f.urun_id == urun_id);

			if (mevcutFavori)
			{
				TempData["ErrorMessage"] = "Bu ürün zaten favorilerinizde.";
				return RedirectToAction("Urunler", "Home");

			}

			var favori = new Favoriler { musteri_id = musteri_id, urun_id = urun_id };
			db.Favoriler.Add(favori);
			db.SaveChanges();
			TempData["SuccessMessage"] = "Ürün favorilere eklendi.";
			return RedirectToAction("Index", "Favoriler");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RemoveFromFavorites(int urun_id)
		{
			var musteri_id = (int?)Session["musteri_id"];

			if (musteri_id == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var favori = db.Favoriler.FirstOrDefault(f => f.musteri_id == musteri_id && f.urun_id == urun_id);

			if (favori == null)
			{
				TempData["ErrorMessage"] = "Bu ürün favorilerinizde değil.";
				return RedirectToAction("Index");
			}

			db.Favoriler.Remove(favori);
			db.SaveChanges();

			TempData["SuccessMessage"] = "Ürün favorilerden kaldırıldı.";
			return RedirectToAction("Index");
		}


		public ActionResult Index()
        {

			var musteri_id = (int?)Session["musteri_id"];

			if (musteri_id == null)
			{
				return RedirectToAction("Hesap", "Home"); 
			}

			var favoriler = db.Favoriler
				.Where(f => f.musteri_id == musteri_id)
				.Select(f => f.Urunler)
				.ToList();

			return View(favoriler);
		}

        // GET: Favoriler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            return View(favoriler);
        }

        // GET: Favoriler/Create
        public ActionResult Create()
        {
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad");
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi");
            return View();
        }

        // POST: Favoriler/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,musteri_id,urun_id")] Favoriler favoriler)
        {
            if (ModelState.IsValid)
            {
                db.Favoriler.Add(favoriler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", favoriler.musteri_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", favoriler.urun_id);
            return View(favoriler);
        }

        // GET: Favoriler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", favoriler.musteri_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", favoriler.urun_id);
            return View(favoriler);
        }

        // POST: Favoriler/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,musteri_id,urun_id")] Favoriler favoriler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", favoriler.musteri_id);
            ViewBag.urun_id = new SelectList(db.Urunler, "urun_id", "urun_adi", favoriler.urun_id);
            return View(favoriler);
        }

        // GET: Favoriler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favoriler favoriler = db.Favoriler.Find(id);
            if (favoriler == null)
            {
                return HttpNotFound();
            }
            return View(favoriler);
        }

        // POST: Favoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favoriler favoriler = db.Favoriler.Find(id);
            db.Favoriler.Remove(favoriler);
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
