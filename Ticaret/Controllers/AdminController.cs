using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ticaret.Controllers
{
    public class AdminController : Controller
    {
    
    
	// GET: Admin
	public ActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Login(string username, string password)
	{
		
		if (username == "admin" && password == "admin123")
		{
			Session["AdminLoggedIn"] = true; 
			return RedirectToAction("Index", "Admin"); 
		}
		else
		{
			ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
			return View();
		}
	}

	public ActionResult Logout()
	{
		Session["AdminLoggedIn"] = null;
		return RedirectToAction("Hesap","Home");
	}

	// Admin ana sayfası
	public ActionResult Index()
	{
		if (Session["AdminLoggedIn"] == null)
		{
			return RedirectToAction("Login");
		}

		return View();
	}
}
}
