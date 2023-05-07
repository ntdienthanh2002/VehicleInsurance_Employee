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
    public class VehicleBrandsController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: VehicleBrands
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View(db.VehicleBrands.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: VehicleBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
                if (vehicleBrand == null)
                {
                    return HttpNotFound();
                }
                ViewBag.VehicleModel = db.VehicleModels.ToList();
                ViewBag.VehicleBrands = db.VehicleBrands.Find(id);
                ViewBag.VehicleModels = db.VehicleModels.Where(d => d.VehicleBrandID == id).FirstOrDefault();
                return View(vehicleBrand);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public String VehicleModels(int id)
        {
            String name = "";
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel != null)
            {
                name = vehicleModel.VehicleModelName;
            }
            return name;
        }
        public String VehicleBrand(int id)
        {
            String name = "";
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            int idB = vehicleModel.VehicleBrandID;
            VehicleBrand br = db.VehicleBrands.Find(idB);
            name = br.VehicleBrandName;
            return name;
        }

        // GET: VehicleBrands/Create
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

        // POST: VehicleBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleBrandID,VehicleBrandName")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.VehicleBrands.Add(vehicleBrand);
                db.SaveChanges();
                TempData["AlertMessage"] = "Vehicle brand created successfully!";
                return RedirectToAction("Index");
            }

            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
                if (vehicleBrand == null)
                {
                    return HttpNotFound();
                }
                return View(vehicleBrand);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: VehicleBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleBrandID,VehicleBrandName")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleBrand).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Vehicle brand updated successfully!";
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
                if (vehicleBrand == null)
                {
                    return HttpNotFound();
                }
                return View(vehicleBrand);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: VehicleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            db.VehicleBrands.Remove(vehicleBrand);
            db.SaveChanges();
            TempData["AlertMessage"] = "Vehicle brand deleted successfully!";
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
