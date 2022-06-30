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
    public class UsersToProjectsController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: UsersToProjects
        public ActionResult Index()
        {
            var assignUsersToProjects = db.AssignUsersToProjects.Include(a => a.Employee).Include(a => a.Project);
            return View(assignUsersToProjects.ToList());
        }

        // GET: UsersToProjects/Details/5
        public ActionResult Details(string EmailId)
        {
            var employeeDetails = (from n in db.AssignUsersToProjects
                                   where n.EmailId == EmailId
                                   select n).FirstOrDefault();

            return View(employeeDetails);
        }

        // GET: UsersToProjects/Create
        public ActionResult Create()
        {
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName");
            ViewBag.ProjectName = new SelectList(db.Projects, "ProjectName", "ProjectDescription");
            return View();
        }

        // POST: UsersToProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmailId,ProjectName")] AssignUsersToProject assignUsersToProject)
        {
            if (ModelState.IsValid)
            {
                db.AssignUsersToProjects.Add(assignUsersToProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToProject.EmailId);
            ViewBag.ProjectName = new SelectList(db.Projects, "ProjectName", "ProjectDescription", assignUsersToProject.ProjectName);
            return View(assignUsersToProject);
        }

        // GET: UsersToProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignUsersToProject assignUsersToProject = db.AssignUsersToProjects.Find(id);
            if (assignUsersToProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToProject.EmailId);
            ViewBag.ProjectName = new SelectList(db.Projects, "ProjectName", "ProjectDescription", assignUsersToProject.ProjectName);
            return View(assignUsersToProject);
        }

        // POST: UsersToProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailId,ProjectName")] AssignUsersToProject assignUsersToProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignUsersToProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmailId = new SelectList(db.Employees, "EmailId", "FirstName", assignUsersToProject.EmailId);
            ViewBag.ProjectName = new SelectList(db.Projects, "ProjectName", "ProjectDescription", assignUsersToProject.ProjectName);
            return View(assignUsersToProject);
        }

        // GET: UsersToProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignUsersToProject assignUsersToProject = db.AssignUsersToProjects.Find(id);
            if (assignUsersToProject == null)
            {
                return HttpNotFound();
            }
            return View(assignUsersToProject);
        }

        // POST: UsersToProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignUsersToProject assignUsersToProject = db.AssignUsersToProjects.Find(id);
            db.AssignUsersToProjects.Remove(assignUsersToProject);
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
