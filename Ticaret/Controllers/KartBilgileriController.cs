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
    public class KartBilgileriController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

		
		public ActionResult KartBilgileri()
		{
			var musteri_id = (int)Session["musteri_id"];
			var musteri = db.KartBilgileri.Find(musteri_id);
            return View(musteri);
		}

		public ActionResult OdemeSecimi(string odemeTipi)
		{
			// Ödeme türünü session veya view bag ile sakla
			Session["odemeTipi"] = odemeTipi;

			return RedirectToAction("Onayla");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Onayla(KartBilgileri kartBilgileri)
		{
			if (ModelState.IsValid)
			{
				using (var context = new ticaretEntities())
				{
					var musteri_id = (int?)Session["musteri_id"];
					if (musteri_id == null)
					{
						
						ModelState.AddModelError("", "Müşteri ID bulunamadı.");
						return View("KartBilgileri", "Sepet");
					}

					kartBilgileri.musteri_id = musteri_id.Value;
					context.KartBilgileri.Add(kartBilgileri);
					context.SaveChanges();


					var sepet = context.Sepet
						.Include(s => s.Urunler)
						.Where(s => s.musteri_id == musteri_id)
						.ToList();

					var siparisDetayList = new List<Siparisdetay>();
					decimal? toplamFiyat = 0;

					foreach (var item in sepet)
					{
						var siparisDetay = new Siparisdetay
						{
							urun_id = item.urun_id,
							adet = item.adet,
							fiyat = item.Urunler.fiyat,
							toplam_fiyat = item.adet * item.Urunler.fiyat
						};
						siparisDetayList.Add(siparisDetay);
						toplamFiyat += siparisDetay.toplam_fiyat;
					}

					var siparis = new Siparisler
					{
						musteri_id = musteri_id,
						siparis_tarihi = DateTime.Now,
						fiyat = toplamFiyat,
						durum = "Siparişiniz Alındı", 
						ad_soyad = context.Musteriler.Find(musteri_id).ad_soyad,
						Adres = context.Musteriler.Find(musteri_id).adres,
						Siparisdetay = siparisDetayList
					};

					context.Siparisler.Add(siparis);
					context.SaveChanges();

					
					var odemeTipi = "Kartla Ödeme";
					var odeme = new Odemeler
					{
						odeme_tipi = odemeTipi,
						siparis_id = siparis.siparis_id
					};

					context.Odemeler.Add(odeme);
					context.SaveChanges();


					siparis.odeme_id = odeme.odeme_id;
					context.Siparisler.Attach(siparis);
					context.Entry(siparis).State = EntityState.Modified;
					context.SaveChanges();

					
					context.Sepet.RemoveRange(sepet);
					context.SaveChanges();
				}

				return RedirectToAction("SiparisTamamlandi", "Sepet"); 
			}

			return View("KartBilgileri");
		}

		
		public ActionResult Adres()
		{
			var musteri_id = (int)Session["musteri_id"];
			var musteri = db.Musteriler.Find(musteri_id);
			return View(musteri);
		}

		// GET: KartBilgileri
		public ActionResult Index()
        {
            var kartBilgileri = db.KartBilgileri.Include(k => k.Musteriler);
            return View(kartBilgileri.ToList());
        }

        // GET: KartBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KartBilgileri kartBilgileri = db.KartBilgileri.Find(id);
            if (kartBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(kartBilgileri);
        }

        // GET: KartBilgileri/Create
        public ActionResult Create()
        {
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad");
            return View();
        }

        // POST: KartBilgileri/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kart_id,kart_numarasi,Cvv,SonKullanmaTarihi,musteri_id,odeme_tipi")] KartBilgileri kartBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.KartBilgileri.Add(kartBilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", kartBilgileri.musteri_id);
            return View(kartBilgileri);
        }

        // GET: KartBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KartBilgileri kartBilgileri = db.KartBilgileri.Find(id);
            if (kartBilgileri == null)
            {
                return HttpNotFound();
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", kartBilgileri.musteri_id);
            return View(kartBilgileri);
        }

        // POST: KartBilgileri/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kart_id,kart_numarasi,Cvv,SonKullanmaTarihi,musteri_id,odeme_tipi")] KartBilgileri kartBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kartBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.musteri_id = new SelectList(db.Musteriler, "musteri_id", "ad_soyad", kartBilgileri.musteri_id);
            return View(kartBilgileri);
        }

        // GET: KartBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KartBilgileri kartBilgileri = db.KartBilgileri.Find(id);
            if (kartBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(kartBilgileri);
        }

        // POST: KartBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KartBilgileri kartBilgileri = db.KartBilgileri.Find(id);
            db.KartBilgileri.Remove(kartBilgileri);
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
