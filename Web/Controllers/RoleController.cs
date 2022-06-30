using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    
    public class RoleController : Controller
    {
        private DatabaseEntities context=new DatabaseEntities();


        //Admin can only view Roles list
        [Authorize(Roles ="Admin")]
        // GET: Role
        public ActionResult List(string search)
        {
            return View(context.Roles.Where(x => x.RoleName.StartsWith(search) || search == null).ToList());
        }

        //Admin can only view details of roles
        [Authorize(Roles ="Admin")]
        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            var roleDetails = (from n in context.Roles
                                   where n.RoleId == id
                                   select n).First();
            return View(roleDetails);
        }
        //Admin can only create the roles

        [Authorize(Roles ="Admin")]
        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
                return View();
            var emp = context.Roles.Where(x => x.RoleName == role.RoleName).ToList();
            if (emp.Count > 0)
            {
                ModelState.AddModelError("", "RoleName is already exists..!");
            }
            else
            {
                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        //Admin can only remove the roles
        [Authorize(Roles ="Admin")]

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            var del = context.Roles.Where(x => x.RoleId == id).First();
            context.Roles.Remove(del);
            context.SaveChanges();

            return RedirectToAction("List");
            
        }
       
        //Admin can only assign users to roles
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AssignUsers()
        {
            
            ViewBag.EmailId = new SelectList(context.Employees.ToList(), "EmailId", "EmailId");
            ViewBag.RoleName = new SelectList(context.Roles.ToList(), "RoleName", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult AssignUsers(AssignUsersToRole usersToRole)
        {
            if (!ModelState.IsValid)
                return View();
            
                context.AssignUsersToRoles.Add(usersToRole);
                context.SaveChanges();
                return RedirectToAction("List");
           
        }
    }
}
