using System.Data;
using System.Linq;
using System.Web.Mvc;
using M1GL2023.Models;
using PagedList;
using System.IO;

namespace M1GL2023.Controllers
{
    public class ProprietairesController : Controller
    {
        private ImmobilierContexte db = new ImmobilierContexte();


        //GET: Proprietaires
        public ActionResult Index(int?page, string Nom, string Prenom)
        {
            int pageSize = 4;
            ViewBag.Nom = Nom;
            ViewBag.Prenom = Prenom;
            page = page.HasValue ? page : 1;
            var list = db.Proprietaires.ToList();

            if (!string.IsNullOrEmpty(Nom))
            {
                list = list.Where(a => a.Nom.ToLower().ToLower().Contains(Nom.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(Prenom))
            {
                list = list.Where(a => a.Prenom.ToLower().ToLower().Contains(Prenom.ToLower())).ToList();
            }


            return View(list.ToPagedList((int)page, pageSize));
        }

        public DataTable GetProprio()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Nom", typeof(string));
            table.Columns.Add("Prenom", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Username", typeof(string));
            var proprietaire = db.Proprietaires.ToList();
            foreach(var e in proprietaire)
                table.Rows.Add(e.Nom, e.Prenom, e.Email, e.Username);
            return table;
        }

    
    public ActionResult imprimer()
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument rpt
           = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            try
            {
                rpt.Load(Server.MapPath("~/Report/rptlisteProprio.rpt"));
                rpt.SetDataSource(GetProprio());
                Stream stream =
               rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.AppendHeader("Content-Disposition", "inline");
                return File(stream, "application/pdf");
            }
            finally
            {
                rpt.Dispose();
                rpt.Close();
            }
        }

        public JsonResult GetProprietaire(int? page, string Nom, string Prenom)
        {
            int pageSize = 1;
            page = page.HasValue ? page : 1;
            var list = db.Proprietaires.ToList();
            if(!string.IsNullOrEmpty(Nom) )
            {
                list = list.Where(a=>a.Nom.ToLower().ToLower().Contains(Nom.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(Prenom))
            {
                list = list.Where(a => a.Prenom.ToLower().ToLower().Contains(Prenom.ToLower())).ToList();
            }

            return Json(list.ToPagedList((int)page, pageSize), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Add(Proprietaire pro)
        {
            db.Proprietaires.Add(pro);
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Proprietaire pro)
        {
            Proprietaire e = db.Proprietaires.Find(pro.Id);
            e.Prenom = pro.Prenom;
            e.Nom = pro.Nom;
            e.Username = pro.Username;
            e.Email = pro.Email;
            e.Password = pro.Password;
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            var proprietaire = db.Proprietaires.ToList().Find(x => x.Id.Equals(ID));
            return Json(proprietaire, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            Proprietaire e = db.Proprietaires.Find(ID);
            db.Proprietaires.Remove(e);
            db.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }




        //// GET: Proprietaires/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Proprietaire proprietaire = (Proprietaire)db.Utilisateurs.Find(id);
        //    if (proprietaire == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(proprietaire);
        //}

        //// GET: Proprietaires/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Proprietaires/Create
        //// Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,Username,Password")] Proprietaire proprietaire)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       db.Utilisateurs.Add(proprietaire);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(proprietaire);
        //}

        //// GET: Proprietaires/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Proprietaire proprietaire = (Proprietaire)db.Utilisateurs.Find(id);
        //    if (proprietaire == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(proprietaire);
        //}

        //// POST: Proprietaires/Edit/5
        //// Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Email,Username,Password")] Proprietaire proprietaire)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(proprietaire).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(proprietaire);
        //}

        //// GET: Proprietaires/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Proprietaire proprietaire = (Proprietaire)db.Utilisateurs.Find(id);
        //    if (proprietaire == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(proprietaire);
        //}

        //// POST: Proprietaires/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Proprietaire proprietaire = (Proprietaire)db.Utilisateurs.Find(id);
        //    db.Utilisateurs.Remove(proprietaire);
        //    db.SaveChanges();
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
