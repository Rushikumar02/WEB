using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UsersToRolesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: UsersToRoles
        public ActionResult Index()
        {
            var assignUsersToRoles = db.AssignUsersToRoles.Include(a => a.Employee).Include(a => a.Role);
            return View(assignUsersToRoles.ToList());
        }

        // GET: UsersToRoles/Details/5
        public ActionResult Details(string EmailId)
        {
            var employeeDetails = (from n in db.AssignUsersToRoles
                                   where n.EmailId == EmailId
                                   select n).FirstOrDefault();

            return View(employeeDetails);
        }

        // GET: UsersToRoles/Create
        public ActionResult Create()
        {
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName");
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName");
            return View();
        }

        // POST: UsersToRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmailId,RoleName")] AssignUsersToRole assignUsersToRole)
        {
            if (ModelState.IsValid)
            {
                db.AssignUsersToRoles.Add(assignUsersToRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToRole.EmailId);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName", assignUsersToRole.RoleName);
            return View(assignUsersToRole);
        }

        // GET: UsersToRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignUsersToRole assignUsersToRole = db.AssignUsersToRoles.Find(id);
            if (assignUsersToRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToRole.EmailId);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName", assignUsersToRole.RoleName);
            return View(assignUsersToRole);
        }

        // POST: UsersToRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailId,RoleName")] AssignUsersToRole assignUsersToRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignUsersToRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToRole.EmailId);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName", assignUsersToRole.RoleName);
            return View(assignUsersToRole);
        }

        // GET: UsersToRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignUsersToRole assignUsersToRole = db.AssignUsersToRoles.Find(id);
            if (assignUsersToRole == null)
            {
                return HttpNotFound();
            }
            return View(assignUsersToRole);
        }

        // POST: UsersToRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignUsersToRole assignUsersToRole = db.AssignUsersToRoles.Find(id);
            db.AssignUsersToRoles.Remove(assignUsersToRole);
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
