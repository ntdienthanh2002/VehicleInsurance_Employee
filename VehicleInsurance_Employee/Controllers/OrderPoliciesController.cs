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
    public class OrderPoliciesController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();

        // GET: OrderPolicies
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                var orderPolicies = db.OrderPolicies.Include(o => o.InsuranceType).Include(o => o.Vehicle);
                return View(orderPolicies.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: OrderPolicies/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["EmpID"] != null)
            {
                List<Bill> listBill = new List<Bill>();
                foreach (Bill bi in db.Bills)
                {
                    if (bi.OrderPolicy.PolicyNumber == id)
                    {
                        Bill bill = new Bill();
                        bill.BillNo = bi.BillNo;
                        bill.PolicyNumber = bi.PolicyNumber;
                        bill.ActualDatetime = bi.ActualDatetime;
                        bill.ExpectedDate = bi.ExpectedDate;
                        bill.Amount = bi.Amount;
                        bill.Status = bi.Status;
                        listBill.Add(bill);
                    }
                }
                ViewBag.Bill = listBill;
                return View(db.OrderPolicies.Find(id));
                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                //OrderPolicy orderPolicy = db.OrderPolicies.Find(id);
                //if (orderPolicy == null)
                //{
                //    return HttpNotFound();
                //}
                //return View(orderPolicy);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        //public String Bills(int id)
        //{
        //    int no;
        //    Bill bill = db.Bills.Find(id);
        //    if (bill != null)
        //    {
        //        no = bill.BillNo;
        //    }
        //    return no;
        //}

        //public String VehicleBrand(int id)
        //{
        //    String name = "";
        //    VehicleModel vehicleModel = db.VehicleModels.Find(id);
        //    int idB = vehicleModel.VehicleBrandID;
        //    VehicleBrand br = db.VehicleBrands.Find(idB);
        //    name = br.VehicleBrandName;
        //    return name;
        //}

        // GET: OrderPolicies/Create
        public ActionResult Create()
        {
            ViewBag.InsuranceTypeID = new SelectList(db.InsuranceTypes, "InsuranceTypeID", "InsuranceTypeName");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "VehicleOwnerName");
            return View();
        }

        // POST: OrderPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PolicyNumber,VehicleID,InsuranceTypeID,OrderDate,PolicyDate,PolicyDuration,PaymentType,Status")] OrderPolicy orderPolicy)
        {
            if (ModelState.IsValid)
            {
                db.OrderPolicies.Add(orderPolicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InsuranceTypeID = new SelectList(db.InsuranceTypes, "InsuranceTypeID", "InsuranceTypeName", orderPolicy.InsuranceTypeID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "VehicleOwnerName", orderPolicy.VehicleID);
            return View(orderPolicy);
        }

        // GET: OrderPolicies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderPolicy orderPolicy = db.OrderPolicies.Find(id);
                if (orderPolicy == null)
                {
                    return HttpNotFound();
                }
                ViewBag.InsuranceTypeID = new SelectList(db.InsuranceTypes, "InsuranceTypeID", "InsuranceTypeName", orderPolicy.InsuranceTypeID);
                ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "VehicleOwnerName", orderPolicy.VehicleID);
                return View(orderPolicy);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: OrderPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PolicyNumber,VehicleID,InsuranceTypeID,OrderDate,PolicyDate,PolicyDuration,PaymentType,Status")] OrderPolicy orderPolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPolicy).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Order policy updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.InsuranceTypeID = new SelectList(db.InsuranceTypes, "InsuranceTypeID", "InsuranceTypeName", orderPolicy.InsuranceTypeID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "VehicleOwnerName", orderPolicy.VehicleID);
            return View(orderPolicy);
        }

        // GET: OrderPolicies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPolicy orderPolicy = db.OrderPolicies.Find(id);
            if (orderPolicy == null)
            {
                return HttpNotFound();
            }
            return View(orderPolicy);
        }

        // POST: OrderPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPolicy orderPolicy = db.OrderPolicies.Find(id);
            db.OrderPolicies.Remove(orderPolicy);
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
