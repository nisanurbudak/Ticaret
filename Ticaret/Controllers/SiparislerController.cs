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
    public class SiparislerController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

		public ActionResult SiparisDetaylari(int id)
		{
			var siparisDetaylari = db.Siparisdetay
		.Where(sd => sd.siparis_id == id)
		.ToList();

			if (siparisDetaylari == null || !siparisDetaylari.Any())
			{
				return HttpNotFound(); 
			}

			var siparis = db.Siparisler.Find(id);

			if (siparis == null)
			{
				return HttpNotFound(); 
			}

			if (siparis.Odemeler.odeme_tipi == "Kartla Ödeme")
			{
				
				var kartBilgileri = db.KartBilgileri
					.FirstOrDefault(kb => kb.musteri_id == siparis.musteri_id);

				var viewModel = new SiparisDetayViewModel
				{
					SiparisId = id,
					SiparisDetaylari = siparisDetaylari,
					KartBilgileri = kartBilgileri
				};

				return View("KartliSiparisDetaylari", viewModel); 
			}
			else
			{
				return View(siparisDetaylari);
			}


		}

		public ActionResult SiparisDetaylari2(int id)
		{
			var siparisDetaylari = db.Siparisdetay
		.Where(sd => sd.siparis_id == id)
		.ToList();

			if (siparisDetaylari == null || !siparisDetaylari.Any())
			{
				return HttpNotFound(); 
			}

			
			var siparis = db.Siparisler.Find(id);
			if (siparis == null)
			{
				return HttpNotFound(); 
			}

			if (siparis.Odemeler.odeme_tipi == "Kartla Ödeme")
			{
			
				var kartBilgileri = db.KartBilgileri
					.FirstOrDefault(kb => kb.musteri_id == siparis.musteri_id);

				var viewModel = new SiparisDetayViewModel
				{
					SiparisId = id,
					SiparisDetaylari = siparisDetaylari,
					KartBilgileri = kartBilgileri
				};

				return View("KartliSiparisDetaylari2", viewModel); 
			}
			else
			{
				return View(siparisDetaylari); 
			}


		}

		public ActionResult KartliSiparisDetaylari()
        {
            return View();
        }

		public ActionResult KartliSiparisDetaylari2()
		{
			return View();
		}

		public ActionResult Index1()
		{
			var musteri_id = (int)Session["musteri_id"];
			var siparisler = db.Siparisler.Where(s => s.musteri_id == musteri_id).ToList();
			return View(siparisler);
		}


		// GET: Siparisler
		public ActionResult Index()
        {
            var siparisler = db.Siparisler.Include(s => s.Musteriler).Include(s => s.Odemeler);
            return View(siparisler.ToList());
        }

        // GET: Siparisler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisler siparisler = db.Siparisler.Find(id);
            if (siparisler == null)
            {
                return HttpNotFound();
            }
            return View(siparisler);
        }

        // GET: Siparisler/Create
        public ActionResult Create()
        {
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad");
            ViewBag.odeme_id = new SelectList(db.Odemeler, "odeme_id", "odeme_tipi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "siparis_id,musteri_id,odeme_id,siparis_tarihi,fiyat,durum,ad_soyad,adres_tanimi,Adres")] Siparisler siparisler )
        {
			if (ModelState.IsValid)
			{
				// Sipariş tarihini ve fiyatını otomatik olarak ayarlama
				siparisler.siparis_tarihi = DateTime.Now;
				
				siparisler.durum = "Yeni"; // Varsayılan sipariş durumu
				db.Siparisler.Add(siparisler);
				db.SaveChanges();


				db.SaveChanges();

				// Sepetteki ürünleri sipariş detaylarına ekleme
				var sepetUrunleri = db.Sepet.Include(s => s.Urunler).Where(s => s.musteri_id == siparisler.musteri_id).ToList();
				foreach (var item in sepetUrunleri)
				{
					Siparisdetay siparisDetay = new Siparisdetay
					{
						urun_id = item.urun_id,
						siparis_id = siparisler.siparis_id,
						fiyat = item.Urunler.fiyat,
						adet = item.adet,
						toplam_fiyat = item.adet * item.Urunler.fiyat
					};
					db.Siparisdetay.Add(siparisDetay);
				}
				db.SaveChanges();

				// Sepeti temizleme
				db.Sepet.RemoveRange(sepetUrunleri);
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", siparisler.musteri_id);
			ViewBag.odeme_id = new SelectList(db.Odemeler, "odeme_id", "odeme_tipi", siparisler.odeme_id);
			return View(siparisler);


		}

        // GET: Siparisler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisler siparisler = db.Siparisler.Find(id);
            if (siparisler == null)
            {
                return HttpNotFound();
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", siparisler.musteri_id);
            ViewBag.odeme_id = new SelectList(db.Odemeler, "odeme_id", "odeme_tipi", siparisler.odeme_id);
            return View(siparisler);
        }

        // POST: Siparisler/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "siparis_id,musteri_id,odeme_id,siparis_tarihi,fiyat,durum,ad_soyad,adres_tanimi,Adres")] Siparisler siparisler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siparisler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", siparisler.musteri_id);
            ViewBag.odeme_id = new SelectList(db.Odemeler, "odeme_id", "odeme_tipi", siparisler.odeme_id);
            return View(siparisler);
        }

        // GET: Siparisler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparisler siparisler = db.Siparisler.Find(id);
            if (siparisler == null)
            {
                return HttpNotFound();
            }
            return View(siparisler);
        }

        // POST: Siparisler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siparisler siparisler = db.Siparisler.Find(id);
            db.Siparisler.Remove(siparisler);
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
