using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M1GL2023.Models;

namespace M1GL2023.Controllers
{
    public class StudioViewModelsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private ImmobilierContexte db = new ImmobilierContexte();

        // GET: StudioViewModels
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<StudioViewModel> GetStudioViewModels()
        {
            List<StudioViewModel> list = new List<StudioViewModel>();
            var listBien = db.Biens.ToList();
            var listStudio = db.Studios.ToList();

            foreach ( var item in listStudio)
            {
                var leBien = listBien.Where(a => a.IdBien == item.IdBien).FirstOrDefault();

                StudioViewModel s = new StudioViewModel();

                s.ProprietaireId = leBien.ProprietaireId;
                s.IdBien = leBien.IdBien;
                s.SuperficiBien = leBien.SuperficiBien;
                s.Description = leBien.Description;
                s.Localisation = leBien.Localisation;
                s.Meuble = item.Meuble;
                list.Add(s);
            }
            return list;
        }
        public ActionResult Index()
        {
            var studioViewModels = GetStudioViewModels();
            return View(studioViewModels.ToList());
        }

        // GET: StudioViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudioViewModel studioViewModel = db.StudioViewModels.Find(id);
            if (studioViewModel == null)
            {
                return HttpNotFound();
            }
            return View(studioViewModel);
        }

        // GET: StudioViewModels/Create
        public ActionResult Create()
        {
            ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom");
            return View();
        }

        // POST: StudioViewModels/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBien,Description,SuperficiBien,Localisation,Meuble")] StudioViewModel studioViewModel)
        {
            if (ModelState.IsValid)
            {
                Studio s = new Studio();

                s.Description = studioViewModel.Description;
                s.SuperficiBien = studioViewModel.SuperficiBien;
                s.Localisation = studioViewModel.Localisation;
                s.Meuble = studioViewModel.Meuble;

                db.Biens.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom", studioViewModel.ProprietaireId);
            return View(studioViewModel);
        }

        // GET: StudioViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudioViewModel studioViewModel = db.StudioViewModels.Find(id);
            if (studioViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom", studioViewModel.ProprietaireId);
            return View(studioViewModel);
        }

        // POST: StudioViewModels/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBien,Description,SuperficiBien,Localisation,Meuble,ProprietaireId")] StudioViewModel studioViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studioViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom", studioViewModel.ProprietaireId);
            return View(studioViewModel);
        }

        // GET: StudioViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudioViewModel studioViewModel = db.StudioViewModels.Find(id);
            if (studioViewModel == null)
            {
                return HttpNotFound();
            }
            return View(studioViewModel);
        }

        // POST: StudioViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudioViewModel studioViewModel = db.StudioViewModels.Find(id);
            db.StudioViewModels.Remove(studioViewModel);
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
