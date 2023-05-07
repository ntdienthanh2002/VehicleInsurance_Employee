using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleInsurance_Employee.Models;

namespace VehicleInsurance_Employee.Controllers
{
    public class CompanyExpensesController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: CompanyExpenses
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View(db.CompanyExpenses.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: CompanyExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyExpense companyExpense = db.CompanyExpenses.Find(id);
            if (companyExpense == null)
            {
                return HttpNotFound();
            }
            return View(companyExpense);
        }

        // GET: CompanyExpenses/Create
        public ActionResult Create()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: CompanyExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyExpenseID,DateOfExpense,TypeOfExpense,AmountOfExpense")] CompanyExpense companyExpense)
        {
            if (ModelState.IsValid)
            {
                db.CompanyExpenses.Add(companyExpense);
                db.SaveChanges();
                TempData["AlertMessage"] = "Company expense created successfully!";
                return RedirectToAction("Index");
            }

            return View(companyExpense);
        }

        // GET: CompanyExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CompanyExpense companyExpense = db.CompanyExpenses.Find(id);
                if (companyExpense == null)
                {
                    return HttpNotFound();
                }
                return View(companyExpense);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: CompanyExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyExpenseID,DateOfExpense,TypeOfExpense,AmountOfExpense")] CompanyExpense companyExpense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyExpense).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Company expense updated successfully!";
                return RedirectToAction("Index");
            }
            return View(companyExpense);
        }

        // GET: CompanyExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CompanyExpense companyExpense = db.CompanyExpenses.Find(id);
                if (companyExpense == null)
                {
                    return HttpNotFound();
                }
                return View(companyExpense);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: CompanyExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyExpense companyExpense = db.CompanyExpenses.Find(id);
            db.CompanyExpenses.Remove(companyExpense);
            db.SaveChanges();
            TempData["AlertMessage"] = "Company expense deleted successfully!";
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
