using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseWork.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace CourseWork.Controllers
{
    public class FacultiesController : Controller
    {
        private studBDEntities db = new studBDEntities();

        // GET: Faculties
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var faculties = db.Faculty.OrderBy(Faculty => Faculty.facultyName);
            return View(faculties.ToPagedList(pageNumber,pageSize));
        }

        // GET: Faculties/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculty.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "facultyName,fullNameOfDirector")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Faculty.Add(faculty);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error!!! The faculty with the same name already exist ");
                }             
            }
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculty.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }
        
        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "facultyName,fullNameOfDirector")] Faculty faculty)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.editFaculty(faculty.facultyName, faculty.fullNameOfDirector);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewBag.error = ex.Message;
                }
                else
                {
                    ViewBag.error = ex.InnerException.Message;
                }
                return View("Error");
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculty.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Faculty faculty = db.Faculty.Find(id);
                db.deleteFaculty(faculty.facultyName);              
                db.SaveChanges();              
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewBag.error = ex.Message;
                }
                else
                {
                    ViewBag.error = ex.InnerException.Message;
                }
                return View("Exception");
            }
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
