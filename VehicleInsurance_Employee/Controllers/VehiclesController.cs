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
    public class VehiclesController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: Vehicles
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                var vehicles = db.Vehicles.Include(v => v.Customer).Include(v => v.VehicleType).Include(v => v.VehicleModel);
                return View(vehicles.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Username");
            ViewBag.VehicleTypeID = new SelectList(db.VehicleTypes, "VehicleTypeID", "VehicleTypeName");
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "VehicleModelName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,VehicleTypeID,VehicleModelID,CustomerID,VehicleOwnerName,VehicleBodyNumber,VehicleEngineNumber,VehicleNumber,Image,SeatNumber,Status")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Username", vehicle.CustomerID);
            ViewBag.VehicleTypeID = new SelectList(db.VehicleTypes, "VehicleTypeID", "VehicleTypeName", vehicle.VehicleTypeID);
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "VehicleModelName", vehicle.VehicleModelID);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = db.Vehicles.Find(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Username", vehicle.CustomerID);
                ViewBag.VehicleTypeID = new SelectList(db.VehicleTypes, "VehicleTypeID", "VehicleTypeName", vehicle.VehicleTypeID);
                ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "VehicleModelName", vehicle.VehicleModelID);
                return View(vehicle);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,VehicleTypeID,VehicleModelID,CustomerID,VehicleOwnerName,VehicleBodyNumber,VehicleEngineNumber,VehicleNumber,Image,SeatNumber,Status")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Vehicle updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Username", vehicle.CustomerID);
            ViewBag.VehicleTypeID = new SelectList(db.VehicleTypes, "VehicleTypeID", "VehicleTypeName", vehicle.VehicleTypeID);
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "VehicleModelName", vehicle.VehicleModelID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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
