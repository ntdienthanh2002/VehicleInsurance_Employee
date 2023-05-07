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
    public class VehicleModelsController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: VehicleModels
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                var vehicleModels = db.VehicleModels.Include(v => v.VehicleBrand);
                return View(vehicleModels.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            if (Session["EmpID"] != null)
            {
                ViewBag.VehicleBrandID = new SelectList(db.VehicleBrands, "VehicleBrandID", "VehicleBrandName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleModelID,VehicleBrandID,VehicleModelName")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.VehicleModels.Add(vehicleModel);
                db.SaveChanges();
                TempData["AlertMessage"] = "Vehicle model created successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.VehicleBrandID = new SelectList(db.VehicleBrands, "VehicleBrandID", "VehicleBrandName", vehicleModel.VehicleBrandID);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleModel vehicleModel = db.VehicleModels.Find(id);
                if (vehicleModel == null)
                {
                    return HttpNotFound();
                }
                ViewBag.VehicleBrandID = new SelectList(db.VehicleBrands, "VehicleBrandID", "VehicleBrandName", vehicleModel.VehicleBrandID);
                return View(vehicleModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleModelID,VehicleBrandID,VehicleModelName")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleModel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Vehicle model updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.VehicleBrandID = new SelectList(db.VehicleBrands, "VehicleBrandID", "VehicleBrandName", vehicleModel.VehicleBrandID);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleModel vehicleModel = db.VehicleModels.Find(id);
                if (vehicleModel == null)
                {
                    return HttpNotFound();
                }
                return View(vehicleModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            db.VehicleModels.Remove(vehicleModel);
            db.SaveChanges();
            TempData["AlertMessage"] = "Vehicle model deleted successfully!";
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
