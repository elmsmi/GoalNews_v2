using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoalNewsV2.Models;

namespace GoalNewsV2.Controllers
{
    public class NewsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: News
        public ActionResult Index(string empleado, string clientee, string fecha, NoticiasViewModel nvm)
        {
            NoticiasViewModel model = new NoticiasViewModel();

            // rellenando los dropdownbox
            var employeesList = db.Employees.Select(x => new { EID = x.EmployeeID, ENAME = x.EmployeeName }).ToList();
            var ClientsList = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            var DatesList = db.News.Select(x => new { Fecha = x.Fecha });
            model.ListOfEmployees = new SelectList(employeesList, "EID", "ENAME");
            model.ListOfClients = new SelectList(ClientsList, "CID", "CNAME");
            model.ListOfDates = new SelectList(DatesList, "Fecha");

            var dbNews = db.News.ToList();
            var dbClients = db.Clients.ToList();

            if (fecha == "")
            {
                fecha = null;
            }
            if (empleado == "")
            {
                empleado = null;
            }
            if (clientee == "")
            {
                clientee = null;
            }

            if (nvm.employeeID != null)
            {
                Employee employee = db.Employees.Where(x => x.EmployeeID == nvm.employeeID).Include(x => x.Clients).FirstOrDefault();


                //model.ListOfNews = (from x in db.News
                //                    where x.Client.ClientID ==  
                //                    where (x.Fecha.Date).ToString() == fecha || fecha == null || fecha == ""
                //                    where x.Client.ClientName == clientee || clientee == null || clientee == ""
                //                    select x ).ToList();
                model.ListOfNews = (from x in db.News select x).ToList();

                //var ClientesIdEmp = (from x in dbClients
                //                     join y in dbEmpCli on x.ClientID equals y.IDCli
                //                     where y.IDEmp == IdEmpleado
                //                     select new { Clis = x.ClientName }).ToList();

                //model.ListOfClients = (from x in dbNews
                //             join y in ClientesIdEmp on x.Cliente equals y.Clis
                //             where x.Cliente == cliente || cliente == null || cliente == ""
                //             where (x.Fecha.Date).ToString() == fecha || fecha == null || fecha == ""
                //             select x).ToList();

                return View(model);

            }
            else
            {
                model.ListOfNews = (from x in dbNews
                                    where x.Client.ClientName == clientee || clientee == null || clientee == ""
                                    where (x.Fecha.Date).ToString() == fecha || fecha == null || fecha == ""
                                    select x).ToList();
                //model.ListOfNews = (from x in dbNews select x).ToList();

                return View(model);
            }

        }

        public ActionResult ExportExcel()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            NoticiasViewModel model = new NoticiasViewModel();
            var ClientsList = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            model.ListOfClients = new SelectList(ClientsList, "CID", "CNAME");
            return View(model);
        }

        // POST: News/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        //public ActionResult Create(New FormData)

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        //public ActionResult Create([Bind(Include = "ID,Noticia,Fecha,Cliente,Empleado")] New @new)
        public ActionResult Create (NoticiasViewModel nvm)
        {
            NoticiasViewModel model = new NoticiasViewModel();
            var ClientsList = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            model.ListOfClients = new SelectList(ClientsList, "CID", "CNAME");

            if (ModelState.IsValid)
            {
                db.News.Add(nvm.noticia);
                Client s = db.Clients.Find(nvm.clientID);
                db.Clients.Add(s);
                db.Clients.Attach(s);
                s.News.Add(nvm.noticia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult CreateEmployee([Bind(Include = "ID,Empleado")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(@new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@new);
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            NoticiasViewModel model = new NoticiasViewModel();
            var ClientsList = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            model.ListOfClients = new SelectList(ClientsList, "CID", "CNAME");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.noticia = db.News.Find(id);
            if (model.noticia == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: News/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(NoticiasViewModel nvm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nvm.noticia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nvm.noticia);
        }

        // GET: News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            New @new = db.News.Find(id);
            db.News.Remove(@new);
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
