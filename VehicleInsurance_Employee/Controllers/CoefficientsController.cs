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
    public class CoefficientsController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: Coefficients
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View(db.Coefficients.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Coefficients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coefficient coefficient = db.Coefficients.Find(id);
            if (coefficient == null)
            {
                return HttpNotFound();
            }
            return View(coefficient);
        }

        // GET: Coefficients/Create
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

        // POST: Coefficients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoefficientID,SeatNumber,Coefficient1")] Coefficient coefficient)
        {
            if (ModelState.IsValid)
            {
                db.Coefficients.Add(coefficient);
                db.SaveChanges();
                TempData["AlertMessage"] = "Coefficient created successfully!";
                return RedirectToAction("Index");
            }

            return View(coefficient);
        }

        // GET: Coefficients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Coefficient coefficient = db.Coefficients.Find(id);
                if (coefficient == null)
                {
                    return HttpNotFound();
                }
                return View(coefficient);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Coefficients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoefficientID,SeatNumber,Coefficient1")] Coefficient coefficient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coefficient).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Coefficient updated successfully!";
                return RedirectToAction("Index");
            }
            return View(coefficient);
        }

        // GET: Coefficients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Coefficient coefficient = db.Coefficients.Find(id);
                if (coefficient == null)
                {
                    return HttpNotFound();
                }
                return View(coefficient);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Coefficients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coefficient coefficient = db.Coefficients.Find(id);
            db.Coefficients.Remove(coefficient);
            db.SaveChanges();
            TempData["AlertMessage"] = "Coefficient deleted successfully!";
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
