using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleInsurance_Employee.Models;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private VehicleInsuranceEntities db = new VehicleInsuranceEntities();
        public ActionResult Index()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //GET: Login
        public ActionResult Login()
        {
            if (Session["EmpID"] != null)
            {
                return RedirectToAction("Index", "Bills");
            }
            else
            {
                return View();
            }
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.Employees.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["EmpID"] = data.FirstOrDefault().EmployeeID;
                    Session["Username"] = data.FirstOrDefault().Username;
                    return RedirectToAction("Index", "Bills");
                }
                else
                {
                    TempData["AlertMessage"] = "Username or password incorrect";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Update personal information
        public ActionResult UpdatePersonal(int? id)
        {
            if (Session["EmpID"] != null)
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
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePersonal([Bind(Include = "EmployeeID,Username,Password,EmployeeName,EmployeeADD,EmployeePhoneNumber,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = "Personal information updated successfully!";
                return RedirectToAction("Index", "Bills");
            }
            return View(employee);
        }

        public ActionResult ChangePassword()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            Employee employee = db.Employees.Find((int)Session["EmpID"]);

            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }
            if (changePassword.OldPassword != employee.Password)
            {
                ModelState.AddModelError("OldPassword", "Old Password is incorrect");
                return View(changePassword);
            }

            if (changePassword.Password == employee.Password)
            {
                ModelState.AddModelError("Password", "Password and Old Password are the same");
                return View(changePassword);
            }

            employee.Password = changePassword.Password;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            TempData["AlertMessage"] = "Password changed successfully!";

            return RedirectToAction("Index", "Bills");
        }

        //Logout
        public ActionResult Logout()
        {
            //remove session
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}