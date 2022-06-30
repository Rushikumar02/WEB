using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private DatabaseEntities context=new DatabaseEntities();   

        //Admin can only view the employees list

        [Authorize(Roles ="Admin")]
        
        [HttpGet]
        public ActionResult List(string search)
        {
            return View(context.Employees.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
        }

        //Admin can only create employees 

        [Authorize(Roles = "Admin")]
        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View();
            var emp=context.Employees.Where(x=> x.EmailId == employee.EmailId).ToList();
            if(emp.Count > 0)
            {
                ModelState.AddModelError("", "Email is already exists..!");
            }
            else
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            


            return View();
           // return RedirectToAction("List");
        }


        //Admin can only edit employee details

        [Authorize(Roles = "Admin")]
        // GET: Employee/Edit/
        public ActionResult Edit(int id)
        {
            var employeeUpdate = (from n in context.Employees
                                  where n.EmployeeId == id
                                  select n).First();
            return View(employeeUpdate);
        }


        // POST: Employee/Edit/
        [HttpPost]
        public ActionResult Edit(Employee employeeUpdate)
        {
            
            if(ModelState.IsValid)
            {
                context.Entry(employeeUpdate).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch(DbEntityValidationException e)
                {
                    Console.WriteLine(e);

                }
                
                return RedirectToAction("List");
            }
            return View(employeeUpdate);
        }

        //Admin can only view the employee details
        [Authorize(Roles = "Admin")]

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
           
            var employeeDetails = (from n in context.Employees
                                   where n.EmployeeId == id
                                   select n).First();
            return View(employeeDetails);
        }

        //Admin can only remove the employees
        [Authorize(Roles = "Admin")]
        // GET: Employee/Delete
        public ActionResult Delete(int id)
        {
            var del = context.Employees.Where(x => x.EmployeeId == id).First();
            context.Employees.Remove(del);
            context.SaveChanges();

            return RedirectToAction("List");

        }
       

    }
}
