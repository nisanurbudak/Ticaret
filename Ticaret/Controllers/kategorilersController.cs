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
    public class kategorilersController : Controller
    {
        private ticaretEntities db = new ticaretEntities();

        // GET: kategorilers
        public ActionResult Index()
        {
            return View(db.kategoriler.ToList());
        }

        // GET: kategorilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kategoriler kategoriler = db.kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // GET: kategorilers/Create
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kategori_id,kategori_ad,kategori_resim")] kategoriler kategoriler, List<HttpPostedFileBase> kategori_resim)
        {
            if (ModelState.IsValid)
            {
				if (kategori_resim != null && kategori_resim.Count > 0)
				{
					var file = kategori_resim[0];
					if (file != null && file.ContentLength > 0)
					{
						var fileName = Path.GetFileName(file.FileName);
						var path = Path.Combine(Server.MapPath("~/Images/kategori"), fileName);
						file.SaveAs(path);
						kategoriler.kategori_resim = "/Images/kategori/" + fileName;
					}
				}
				db.kategoriler.Add(kategoriler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategoriler);
        }

        // GET: kategorilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kategoriler kategoriler = db.kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kategori_id,kategori_ad,kategori_resim")] kategoriler kategoriler, HttpPostedFileBase newKategori_resim)
        {
            if (ModelState.IsValid)
            {
				if (newKategori_resim != null && newKategori_resim.ContentLength > 0)
				{
					var fileName = Path.GetFileName(newKategori_resim.FileName);
					var path = Path.Combine(Server.MapPath("~/Images/kategori"), fileName);
					newKategori_resim.SaveAs(path);
					kategoriler.kategori_resim = "/Images/kategori/" + fileName;
				}
				db.Entry(kategoriler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategoriler);
        }

        // GET: kategorilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kategoriler kategoriler = db.kategoriler.Find(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: kategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kategoriler kategoriler = db.kategoriler.Find(id);
            db.kategoriler.Remove(kategoriler);
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
