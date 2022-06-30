using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private DatabaseEntities context = new DatabaseEntities();


        // Admin and Manager can view Project list page
        [Authorize(Roles = "Admin,Manager")]
        // GET: Projects
        public ActionResult List(string search)
        {
            return View(context.Projects.Where(x => x.ProjectName.StartsWith(search) || search == null).ToList());
        }


        //Manager can only view Project Details
        [Authorize(Roles = "Manager")]
        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            var projectDetails = (from n in context.Projects
                                   where n.ProjectId == id
                                   select n).First();
            return View(projectDetails);
        }

        //Manager can only create Projects
        [Authorize(Roles = "Manager")]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
                return View();
            var emp = context.Projects.Where(x => x.ProjectId == project.ProjectId).ToList();
            if (emp.Count > 0)
            {
                ModelState.AddModelError("", "ProjectName is already exists..!");
            }
            else
            {
                context.Projects.Add(project);
                context.SaveChanges();
                return RedirectToAction("List");
            }



            return View();
        }

        //Mamager can only edit the project details
        [Authorize(Roles = "Manager")]

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            var projectUpdate = (from n in context.Projects
                                  where n.ProjectId == id
                                  select n).First();
            return View(projectUpdate);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Project project)
        {
            if (ModelState.IsValid)
            {
                context.Entry(project).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);

                }

                return RedirectToAction("List");
            }
            return View(project);
        }

        //Manager can only remove th projects

        [Authorize(Roles = "Manager")]

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            var del = context.Projects.Where(x => x.ProjectId == id).First();
            context.Projects.Remove(del);
            context.SaveChanges();

            return RedirectToAction("List");
        }

        //Manager can only assign Employees to projects
        [Authorize(Roles ="Manager")]
        [HttpGet]
        public ActionResult AssignUsers()
        {

            ViewBag.EmailId = new SelectList(context.Employees.ToList(), "EmailId", "EmailId");
            ViewBag.ProjectName = new SelectList(context.Projects.ToList(), "ProjectName", "ProjectName");
            return View();
        }
        [HttpPost]
        public ActionResult AssignUsers(AssignUsersToProject usersToProject)
        {
            if (!ModelState.IsValid)
                return View();
            var emp = context.AssignUsersToProjects.Where(x => x.EmailId == usersToProject.EmailId && x.ProjectName == usersToProject.ProjectName).ToList();
            if (emp.Count > 0)
            {
                ModelState.AddModelError("", "UserName is already exists..!");
            }
            else
            {
                context.AssignUsersToProjects.Add(usersToProject);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

    }
}
