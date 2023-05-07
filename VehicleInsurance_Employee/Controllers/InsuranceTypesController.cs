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
    public class InsuranceTypesController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: InsuranceTypes
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View(db.InsuranceTypes.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: InsuranceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                InsuranceType insuranceType = db.InsuranceTypes.Find(id);
                if (insuranceType == null)
                {
                    return HttpNotFound();
                }
                return View(insuranceType);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: InsuranceTypes/Create
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

        // POST: InsuranceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "InsuranceTypeID,InsuranceTypeName,Price,Description")] InsuranceType insuranceType)
        {
            if (ModelState.IsValid)
            {
                db.InsuranceTypes.Add(insuranceType);
                db.SaveChanges();
                TempData["AlertMessage"] = "Insurance type created successfully!";
                return RedirectToAction("Index");
            }

            return View(insuranceType);
        }

        // GET: InsuranceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                InsuranceType insuranceType = db.InsuranceTypes.Find(id);
                if (insuranceType == null)
                {
                    return HttpNotFound();
                }
                return View(insuranceType);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: InsuranceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsuranceTypeID,InsuranceTypeName,Price,Description")] InsuranceType insuranceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuranceType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Insurance type updated successfully!";
                return RedirectToAction("Index");
            }
            return View(insuranceType);
        }

        // GET: InsuranceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                InsuranceType insuranceType = db.InsuranceTypes.Find(id);
                if (insuranceType == null)
                {
                    return HttpNotFound();
                }
                return View(insuranceType);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: InsuranceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InsuranceType insuranceType = db.InsuranceTypes.Find(id);
            db.InsuranceTypes.Remove(insuranceType);
            db.SaveChanges();
            TempData["AlertMessage"] = "Insurance type deleted successfully!";
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
