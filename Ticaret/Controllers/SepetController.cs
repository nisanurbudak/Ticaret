
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ticaret.Models;

namespace Ticaret.Controllers
{
	public class SepetController : Controller
	{
		private ticaretEntities db = new ticaretEntities();

		public ActionResult SiparisOlustur()
		{
			var musteri_id = (int)Session["musteri_id"];
			var musteri = db.Musteriler.Find(musteri_id);
			var sepet = db.Sepet.Include(s => s.Urunler).Where(s => s.musteri_id == musteri_id).ToList();

			
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
				ad_soyad = musteri.ad_soyad,
				Adres = musteri.adres,
				Siparisdetay = siparisDetayList
			};

			db.Siparisler.Add(siparis);
			db.SaveChanges();

			
			var odemeTipi = Session["odemeTipi"].ToString();

		
			var odeme = new Odemeler
			{
				odeme_tipi = odemeTipi,
				siparis_id = siparis.siparis_id
			};

			db.Odemeler.Add(odeme);
			db.SaveChanges();

			
			siparis.odeme_id = odeme.odeme_id;
			db.Entry(siparis).State = EntityState.Modified;
			db.SaveChanges();

			
			db.Sepet.RemoveRange(sepet);
			db.SaveChanges();

			return RedirectToAction("SiparisTamamlandi");
		}

		
		public ActionResult SiparisTamamlandi()
		{
			return View();
		}

		[HttpPost]
		public ActionResult OdemeSecimi(string odemeTipi)
		{
			Session["odemeTipi"] = odemeTipi;

			return RedirectToAction("SiparisOlustur");
		}
		public ActionResult Adres()
		{
			var musteri_id = (int)Session["musteri_id"];
			var musteri = db.Musteriler.Find(musteri_id);
			return View(musteri);
		}

		public ActionResult Index()
		{
			
			var musteri_id = (int)Session["musteri_id"];
			var sepet = db.Sepet
				.Include(s => s.Musteriler)
				.Include(s => s.Urunler)
				.Where(s => s.musteri_id == musteri_id)
				.ToList();
			return View(sepet);
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			var sepetUrunSayisi = db.Sepet.Count();
			ViewBag.SepetUrunSayisi = sepetUrunSayisi;
		}

		public ActionResult SepeteEkle()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SepeteEkle(int? urun_id, int adet = 1)
		{
			if (urun_id == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var musteri_id = (int)Session["musteri_id"];
			var sepetItem = await db.Sepet
				.FirstOrDefaultAsync(c => c.musteri_id == musteri_id && c.urun_id == urun_id);
			if (sepetItem != null)
			{
				sepetItem.adet += adet;
			}
			else
			{
				sepetItem = new Sepet
				{
					musteri_id = musteri_id,
					urun_id = urun_id.Value,
					adet = adet
				};
				db.Sepet.Add(sepetItem);
			}

			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<ActionResult> SepettenSil(int? sepet_id)
		{
		
			if (sepet_id == null)
			{
				return RedirectToAction("Index", "Home");
			}

			Sepet sepetItem = await db.Sepet.FindAsync(sepet_id);

			if (sepetItem != null)
			{
				db.Sepet.Remove(sepetItem);
				await db.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<ActionResult> AdetiGüncelle(int? sepet_id, int? adet)
		{
			if (sepet_id == null)
			{
				System.Diagnostics.Debug.WriteLine("sepet_id is null");
				return RedirectToAction("Index", "Home");
			}

			if (adet == null || adet < 1)
			{
				adet = 1;
			}

		 
			var sepetItem = await db.Sepet.FindAsync(sepet_id);
			if (sepetItem == null)
			{
				HttpNotFound();
				
			}
			sepetItem.adet = adet.Value;
			await db.SaveChangesAsync();

			return RedirectToAction("Index");
		}

	}

}

	