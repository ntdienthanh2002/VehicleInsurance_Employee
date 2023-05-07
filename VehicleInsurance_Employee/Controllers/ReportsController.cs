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

    public class ReportsController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();
        // GET: Reports
        public ActionResult Index()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Report1()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<OrderPolicy> orderPolicy = db.OrderPolicies.ToList();
                IList<ViewReport> reportList = new List<ViewReport>();
                for (int i = 1; i <= 12; i++)
                {
                    int count = 0;
                    foreach (OrderPolicy o in orderPolicy)
                    {
                        if (o.OrderDate.Month == i)
                        {
                            count++;
                        }
                    }
                    reportList.Add(new ViewReport(i + "/" + DateTime.Today.Year, count));
                }

                return View(reportList.ToList());
            }
        }
        public ActionResult Report2()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<OrderPolicy> orderPolicies = db.OrderPolicies.ToList();
                List<Vehicle> vehicles = db.Vehicles.ToList();
                List<VehicleType> vehicleTypes = db.VehicleTypes.ToList();
                IList<ViewReport> reportList = new List<ViewReport>();
                foreach (VehicleType t in vehicleTypes)
                {
                    int count = 0;
                    foreach (Vehicle v in vehicles)
                    {
                        if (t.VehicleTypeID == v.VehicleTypeID)
                        {
                            foreach (OrderPolicy o in orderPolicies)
                            {
                                if (o.VehicleID == v.VehicleID)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                    reportList.Add(new ViewReport(t.VehicleTypeName, count));
                }
                return View(reportList.ToList());
            }
        }

        public ActionResult Report3()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var claims = db.Claims.Include(c => c.OrderPolicy);
                return View(claims.ToList());
            }
        }
        public ActionResult Report4()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                IList<OrderPolicy> policyList = new List<OrderPolicy>();
                List<OrderPolicy> orderPolicies = db.OrderPolicies.ToList();
                foreach (OrderPolicy o in orderPolicies)
                {
                    if (o.PolicyDate.AddYears(o.PolicyDuration).AddDays(30) > DateTime.Today)
                    {
                        policyList.Add(o);
                    }
                }
                return View(policyList.ToList());
            }
        }
        public ActionResult Report5()
        {
            if (Session["EmpID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                IList<OrderPolicy> policyList = new List<OrderPolicy>();
                List<OrderPolicy> orderPolicies = db.OrderPolicies.ToList();
                foreach (OrderPolicy o in orderPolicies)
                {
                    if (o.PolicyDate.AddYears(o.PolicyDuration) <= DateTime.Today)
                    {
                        policyList.Add(o);
                    }
                }
                return View(policyList.ToList());
            }
        }
    }
}