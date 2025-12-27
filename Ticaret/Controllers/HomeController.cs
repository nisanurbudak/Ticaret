using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Ticaret.Models;

namespace Ticaret.Controllers
{
	public class HomeController : Controller
	{
		private ticaretEntities db = new ticaretEntities();
		public ActionResult Index()
		{
			var saticilar =db.Saticilar.ToList();
		
			if (Session["kullanici_adi"] != null)
			{
				ViewBag.KullaniciAdi = Session["kullanici_adi"].ToString();
			}
			else
			{
				ViewBag.KullaniciAdi = "Misafir";
			}

			return View(saticilar);
		}

		public ActionResult Urunler(int? id)
		{

			var urunler = db.Urunler.Where(u => u.satici_id == id).ToList();
			return View(urunler);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			

			return View();
		}

		public ActionResult Hesap()
		{

			return View();
		}

		[HttpPost]
		public ActionResult Autherize(Ticaret.Models.Musteriler MusterilerModel)
		{
			using(ticaretEntities db = new ticaretEntities())
			{
				var MusterilerDetails = db.Musteriler.Where(x => x.kullanici_adi == MusterilerModel.kullanici_adi && x.sifre == MusterilerModel.sifre).FirstOrDefault();
			    if(MusterilerDetails==null)
				{
					MusterilerModel.LoginErrorMessage = "Kullanıcı adı veya şifre yanlış.";
					return View("Hesap", MusterilerModel);
				}
				else
				{
					Session["musteri_id"] = MusterilerDetails.musteri_id;
					Session["kullanici_adi"] = MusterilerDetails.kullanici_adi;
					return RedirectToAction("Index", "Home");
				}
			}
		
		}
		public ActionResult LogOut()
		{
			int musteri_id = (int)Session["musteri_id"];
			Session.Abandon();
			return RedirectToAction("Hesap", "Home");
		}

		public ActionResult Hesabım()
		{
			
			if (Session["musteri_id"] == null)
			{
				return RedirectToAction("Login", "Home"); 
			}

			int musteri_id = (int)Session["musteri_id"];
			var musteri = db.Musteriler.Find(musteri_id);

			if (musteri == null)
			{
				return HttpNotFound();
			}

			return View(musteri); 
		}
	}
}