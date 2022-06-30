using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    
    public class LoginController : Controller
    {
        private DatabaseEntities context=new DatabaseEntities();

        // GET: Login
        
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Employee employee)
        {
            using (var context = new DatabaseEntities())
            {
                bool Login = context.Employees.Any(x => x.EmailId == employee.EmailId && x.Password == employee.Password);
                //TempData["EmailId"] = employee.EmailId; 
                if (Login)
                {
                    FormsAuthentication.SetAuthCookie(employee.EmailId, false);
                    
                    return RedirectToAction("List", "Employee");
                }
                ModelState.AddModelError("", "Invalid username and password");
                return View();

            }
           
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult MyProfile(string userName)
        {

            var employeeDetails = (from n in context.Employees
                                   where n.EmailId == userName
                                   select n).FirstOrDefault();
            
            return View(employeeDetails);
        }
    }
}
