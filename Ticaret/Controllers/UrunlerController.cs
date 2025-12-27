using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ticaret.Models;

namespace Ticaret.Controllers
{
    public class UrunlerController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.kategoriler).Include(u => u.Saticilar);
            return View(urunler.ToList());
        }

		public ActionResult Index1(string searchQuery, int? kategori_id)
        {
			var urunler = db.Urunler.Include(u => u.kategoriler).ToList();

			if (!string.IsNullOrEmpty(searchQuery))
			{
				urunler = urunler.Where(u => u.urun_adi.ToLower().Contains(searchQuery)).ToList();
			}

			if (kategori_id.HasValue)
			{
				urunler = urunler.Where(u => u.kategori_id == kategori_id.Value).ToList();
				ViewBag.SelectedKategori = db.kategoriler.Find(kategori_id.Value)?.kategori_ad;
			}

			ViewBag.kategoriler = db.kategoriler.ToList();
			ViewBag.UrunSayisi = urunler.Count;
			return View(urunler);
		}
		
		
		// GET: Urunler/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunler/Create
        public ActionResult Create()
        {
            ViewBag.kategori_id = new SelectList(db.kategoriler, "kategori_id", "kategori_ad");
            ViewBag.satici_id = new SelectList(db.Saticilar, "satici_id", "sirket_adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "urun_id,urun_adi,aciklamasi,fiyat,stok,satici_id,kategori_id,picture")] Urunler urunler,List<HttpPostedFileBase> picture)
        {
            if (ModelState.IsValid)
            {
				if (picture != null && picture.Count > 0)
				{
					var file = picture[0];
					if (file != null && file.ContentLength > 0)
					{
						var fileName = Path.GetFileName(file.FileName);
						var path = Path.Combine(Server.MapPath("~/Images/Products"), fileName);
						file.SaveAs(path);
						urunler.picture = "/Images/Products/" + fileName;
					}
				}
				db.Urunler.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kategori_id = new SelectList(db.kategoriler, "kategori_id", "kategori_ad", urunler.kategori_id);
            ViewBag.satici_id = new SelectList(db.Saticilar, "satici_id", "sirket_adi", urunler.satici_id);
            return View(urunler);
        }

        // GET: Urunler/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Urunler urunler = db.Urunler.Find(id);
			if (urunler == null)
			{
				return HttpNotFound();
			}

			ViewBag.kategori_id = new SelectList(db.kategoriler, "kategori_id", "kategori_ad", urunler.kategori_id);
			ViewBag.satici_id = new SelectList(db.Saticilar, "satici_id", "sirket_adi", urunler.satici_id);

			return View(urunler);
		}

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "urun_id,urun_adi,aciklamasi,fiyat,stok,satici_id,kategori_id,picture")] Urunler urunler, HttpPostedFileBase newPicture)
        {
            if (ModelState.IsValid)
            {
				if (newPicture != null && newPicture.ContentLength > 0)
				{
					var fileName = Path.GetFileName(newPicture.FileName);
					var path = Path.Combine(Server.MapPath("~/Images/Products"), fileName);
					newPicture.SaveAs(path);
					urunler.picture = "/Images/Products/" + fileName;
				}
				db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kategori_id = new SelectList(db.kategoriler, "kategori_id", "kategori_ad", urunler.kategori_id);
            ViewBag.satici_id = new SelectList(db.Saticilar, "satici_id", "sirket_adi", urunler.satici_id);
            return View(urunler);
        }

        // GET: Urunler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public ActionResult Details1(int id)
		{
			var urun = db.Urunler.Find(id);
			if (urun == null)
			{
				return HttpNotFound();
			}
			return View(urun);
		}

		public ActionResult Details3(int id)
		{
			var urun = db.Urunler.Find(id);
			if (urun == null)
			{
				return HttpNotFound();
			}
			return View(urun);
		}
		public ActionResult Details2(int id)
		{
			var urun = db.Urunler.Find(id);
			if (urun == null)
			{
				return HttpNotFound();
			}
			return View(urun);
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