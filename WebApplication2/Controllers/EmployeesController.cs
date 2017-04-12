using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoalNewsV2.Models;

namespace GoalNewsV2.Controllers
{
    public class EmployeesController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: Employees
        [Authorize]
        public ActionResult Index()
        {
            //List<Aux> model = new List<Aux>();

            //var empleados = db.Employees.ToList();
            //var empcli = db.EmpClis.ToList();
            //var clientes = db.Clients.ToList();
            //var q = from x in empleados.AsEnumerable()
            //        join y in empcli.AsEnumerable() on x.ID equals y.IDEmp
            //        join z in clientes.AsEnumerable() on y.IDCli equals z.ID
            //        select new { empA = x, cliA = z };

            //foreach (var item in q)
            //{
            //    model.Add(new Aux()
            //    {
            //        empAux = item.empA.Empleado,
            //        empIdAux = item.empA.ID,
            //        cliAux = item.cliA.Cliente
            //    });
            //}
            //return View(model.OrderBy(p => p.empAux));

            return View(db.Employees.ToList());

        }

        // GET: Employees/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee employee = db.Employees.Find(id);
            Employee employee = db.Employees.Where(x => x.EmployeeID == id).Include(x => x.Clients).FirstOrDefault();

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public ActionResult Create()
        {
            EmpCliViewModel model = new EmpCliViewModel();
            var d = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            model.ListOfClients = new MultiSelectList(d, "CID", "CNAME");
            return View(model);

        }

        // POST: Employees/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(EmpCliViewModel ecvm)

        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(ecvm.employee);
                if( ecvm.SelectedClients != null)
                {
                    foreach (var x in ecvm.SelectedClients)
                    {
                        Client s = new Client { ClientID = x };
                        db.Clients.Add(s);
                        db.Clients.Attach(s);
                        ecvm.employee.Clients.Add(s);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ecvm.employee);
        }

        // GET: Employees/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            EmpCliViewModel ecvm = new EmpCliViewModel();
            var d = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            ecvm.ListOfClients = new MultiSelectList(d, "CID", "CNAME");
            ecvm.employee = db.Employees.Find(id);
            if (ecvm.employee == null)
            {
                return HttpNotFound();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(ecvm);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(EmpCliViewModel ecvm)
        {
            // Pasamos todos los clientes para el dropdown (para el caso de errores)
            var d = db.Clients.Select(x => new { CID = x.ClientID, CNAME = x.ClientName }).ToList();
            ecvm.ListOfClients = new MultiSelectList(d, "CID", "CNAME");

            if (ModelState.IsValid)
            {
                db.Entry(ecvm.employee).State = EntityState.Modified;
                if (ecvm.SelectedClients != null)
                {
                    foreach (var x in ecvm.SelectedClients)
                    {
                        Client s = new Client { ClientID = x };
                        db.Clients.Add(s);
                        db.Clients.Attach(s);
                        ecvm.employee.Clients.Add(s);
                    }
                }
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);

            }
            return View(ecvm.employee);
        }



        // GET: Employees/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {

            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            foreach (Employee emp in db.Employees)
            {
                db.Employees.Remove(emp);
            }

            return RedirectToAction("Index");

        }

        public ActionResult deleteClientFromEmployee(int? cliId, int? empId)
        {
            if(cliId !=null && empId != null)
            {
                var employee = db.Employees.Find(empId);
                var client = db.Clients.Find(cliId);
                employee.Clients.Remove(client);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return RedirectToAction("ServerError", "Error");
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
