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
    public class CustomersController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();
        // GET: Customers
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View(db.Customers);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Customers/Details/5 
        public ActionResult Details(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Vehicle = db.Vehicles.ToList();
                ViewBag.Customers = db.Customers.Find(id);
                ViewBag.OrderPolicy = db.OrderPolicies.Where(d => d.VehicleID == id).FirstOrDefault();
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public String VehicleType(int id)
        {
            string name = "";
            VehicleType vehicleType = db.VehicleTypes.Find(id);
            if (vehicleType != null)
            {
                name = vehicleType.VehicleTypeName;
            }
            return name;

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
        // GET: Customers/Create
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

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Username,Password,CustomerName,CustomerADD,CustomerPhoneNumber,Email,CitizenIdentityCard,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Username,Password,CustomerName,CustomerADD,CustomerPhoneNumber,Email,CitizenIdentityCard,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Status updated successfully!";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["EmpID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
        public ActionResult OrderViews(int id)
        {
            if (Session["EmpID"] != null)
            {
                List<OrderPolicy> listorder = new List<OrderPolicy>();
                foreach (OrderPolicy order in db.OrderPolicies)
                {
                    if (order.Vehicle.CustomerID == id)
                    {
                        OrderPolicy policy = new OrderPolicy();
                        policy.PolicyNumber = order.PolicyNumber;
                        policy.Vehicle = order.Vehicle;
                        policy.InsuranceTypeID = order.InsuranceTypeID;
                        policy.InsurancePrice = order.InsurancePrice;
                        policy.OrderDate = order.OrderDate;
                        policy.PolicyDate = order.PolicyDate;
                        policy.PolicyDuration = order.PolicyDuration;
                        policy.PaymentType = order.PaymentType;
                        policy.Status = order.Status;
                        listorder.Add(policy);
                    }
                }
                ViewBag.policyorder = listorder;
                List<Bill> listbill = new List<Bill>();
                foreach (Bill bi in db.Bills)
                {
                    if (bi.OrderPolicy.Vehicle.CustomerID == id)
                    {
                        Bill bill = new Bill();
                        bill.BillNo = bi.BillNo;
                        bill.PolicyNumber = bi.PolicyNumber;
                        bill.ActualDatetime = bi.ActualDatetime;
                        bill.ExpectedDate = bi.ExpectedDate;
                        bill.Amount = bi.Amount;
                        bill.Status = bi.Status;
                        listbill.Add(bill);
                    }
                }
                ViewBag.Bill = listbill;
                return View(db.Customers.Find(id));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public bool checkcustomer(int customerid, int vhcid)
        {
            bool check = false;
            Vehicle vehicle = db.Vehicles.Find(vhcid);
            if (vehicle != null && vehicle.CustomerID == customerid)
            {
                check = true;
            }
            return check;
        }

        private bool check(List<int> li, int id)
        {
            bool kt = true;
            foreach (int i in li)
            {
                if (i == id)
                {
                    kt = false;
                }
            }
            return kt;
        }
    }
}
