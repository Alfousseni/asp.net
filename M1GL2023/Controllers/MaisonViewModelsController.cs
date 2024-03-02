using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using M1GL2023.Models;
using System.Web.UI;

namespace M1GL2023.Controllers
{
    public class MaisonViewModelsController : Controller
    {
        private ImmobilierContexte db = new ImmobilierContexte();

            private List<MaisonViewModel> GetMaisonViewModels()
            {
            List<MaisonViewModel> liste = new List<MaisonViewModel>();

            var listeMaison = db.Maisons.ToList();
            var listeBien = db.Biens.ToList();

            foreach (var item in listeMaison)
            {
                var leBien = listeBien.Where(a => a.IdBien == item.IdBien).FirstOrDefault();
                MaisonViewModel m = new MaisonViewModel();
                m.IdBien = leBien.IdBien;
                m.Localisation = leBien.Localisation;
                m.Proprietaire = leBien.Proprietaire;
                m.SuperficiBien = leBien.SuperficiBien;
                m.NbreCuisine = item.NbreCuisine;
                m.Description = leBien.Description;
                m.NbreChambre = item.NbreChambre;
                m.NbreSalleEau = item.NbreSalleEau;
                m.NbreToilete = item.NbreToilete;
                liste.Add(m);

            }
            return liste;
        }

        // GET: MaisonViewModels
        public ActionResult Index(int? page, string NbreSalleEau, string NbreChambre)
        {
            int pageSize = 10;
            ViewBag.Proprietaires = db.Proprietaires.ToList();
            page = page.HasValue ? page : 1;
            var list = GetMaisonViewModels();
            if (int.TryParse(NbreSalleEau, out int nombreSalleEau) && int.TryParse(NbreChambre, out int nombreChambre))
            {
                ViewBag.NbreSalleEau = nombreSalleEau;
                ViewBag.NbrChambre = nombreChambre;
                if (nombreSalleEau != 0)
                {
                    list = list.Where(a => a.NbreSalleEau == nombreSalleEau).ToList();
                }

                if (nombreChambre != 0)
                {
                    list = list.Where(a => a.NbreChambre == nombreChambre).ToList();
                }
            }
            else
            {
                // Échec de la conversion
            }

            return View(list.ToPagedList((int)page, pageSize));
        }

        public JsonResult Add(MaisonViewModel maisonViewModel)
        {
            

                Maison m = new Maison();

                m.NbreCuisine = maisonViewModel.NbreCuisine;
                m.NbreChambre = maisonViewModel.NbreChambre;
                m.Description = maisonViewModel.Description;
                m.Localisation = maisonViewModel.Localisation;
                m.NbreSalleEau = maisonViewModel.NbreSalleEau;
                m.NbreToilete = maisonViewModel.NbreToilete;
                m.SuperficiBien = maisonViewModel.SuperficiBien;
                db.Biens.Add(m);
                db.SaveChanges();
            
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(MaisonViewModel maisonViewModel)
        {
            Maison m = db.Maisons.Find(maisonViewModel.IdBien);
            if (m != null)
            {
                m.NbreCuisine = maisonViewModel.NbreCuisine;
                m.NbreChambre = maisonViewModel.NbreChambre;
                m.Description = maisonViewModel.Description;
                m.Localisation = maisonViewModel.Localisation;
                m.NbreSalleEau = maisonViewModel.NbreSalleEau;
                m.NbreToilete = maisonViewModel.NbreToilete;
                m.SuperficiBien = maisonViewModel.SuperficiBien;
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetbyID(int ID)
        {
            var maison = db.Maisons.ToList().Find(x => x.IdBien.Equals(ID));
            return Json(maison, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            Maison e = db.Maisons.Find(ID);
            db.Maisons.Remove(e);
            db.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }


        //// GET: MaisonViewModels/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaisonViewModel maisonViewModel = null;// db.MaisonViewModels.Find(id);
        //    if (maisonViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maisonViewModel);
        //}

        //// GET: MaisonViewModels/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Nom");
        //    return View();
        //}

        //// POST: MaisonViewModels/Create
        //// Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdBien,Description,SuperficiBien,Localisation,ProprietaireId,NbreChambre,NbreCuisine,NbreSalleEau,NbreToilete")] MaisonViewModel maisonViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.MaisonViewModels.Add(maisonViewModel);

        //        Maison m = new Maison();

        //        m.NbreCuisine = maisonViewModel.NbreCuisine;
        //        m.NbreChambre = maisonViewModel.NbreChambre;
        //        m.Description = maisonViewModel.Description;
        //        m.Localisation = maisonViewModel.Localisation;
        //        m.NbreSalleEau = maisonViewModel.NbreSalleEau;
        //        m.NbreToilete = maisonViewModel.NbreToilete;
        //        m.SuperficiBien = maisonViewModel.SuperficiBien;
        //        db.Biens.Add(m);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProprietaireId = new SelectList(db.Proprietaires, "Id", "Nom", maisonViewModel.ProprietaireId);
        //    return View(maisonViewModel);
        //}

        //// GET: MaisonViewModels/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaisonViewModel maisonViewModel = null;// db.MaisonViewModels.Find(id);
        //    if (maisonViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom", maisonViewModel.ProprietaireId);
        //    return View(maisonViewModel);
        //}

        //// POST: MaisonViewModels/Edit/5
        //// Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdBien,Description,SuperficiBien,Localisation,ProprietaireId,NbreChambre,NbreCuisine,NbreSalleEau,NbreToilete")] MaisonViewModel maisonViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(maisonViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.ProprietaireId = new SelectList(db.Utilisateurs, "Id", "Nom", maisonViewModel.ProprietaireId);
        //    return View(maisonViewModel);
        //}

        //// GET: MaisonViewModels/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaisonViewModel maisonViewModel = null;// db.MaisonViewModels.Find(id);
        //    if (maisonViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maisonViewModel);
        //}

        //// POST: MaisonViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    //MaisonViewModel maisonViewModel = null;// db.MaisonViewModels.Find(id);
        //    //db.MaisonViewModels.Remove(maisonViewModel);
        //    //db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
