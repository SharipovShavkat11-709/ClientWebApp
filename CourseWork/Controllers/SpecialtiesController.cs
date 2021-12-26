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
    public class SpecialtiesController : Controller
    {
        private studBDEntities db = new studBDEntities();

        // GET: Specialties
        public ActionResult Index(int? page)
        {           
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var specialty = db.Specialty.OrderBy(Specialty => Specialty.specialtyCode);
            return View(specialty.ToPagedList(pageNumber, pageSize));
        }

        // GET: Specialties/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialty.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Specialties/Create
        public ActionResult Create()
        {
            ViewBag.facultyName = new SelectList(db.Faculty, "facultyName", "facultyname");
            return View();
        }

        // POST: Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]      
        public ActionResult Create([Bind(Include = "specialtyCode,facultyName,qualification")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Specialty.Add(specialty);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error!!! The specialty with the same code already exist ");
                }
            }
            ViewBag.facultyName = new SelectList(db.Faculty, "facultyName", "facultyName", specialty.facultyName);
            return View(specialty);
        }

        // GET: Specialties/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialty.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            ViewBag.facultyName = new SelectList(db.Faculty, "facultyName", "facultyName", specialty.facultyName);
            return View(specialty);
        }

        // POST: Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "specialtyCode,facultyName,qualification")] Specialty specialty)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.editSpecialty(specialty.specialtyCode, specialty.facultyName, specialty.qualification);
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
            ViewBag.facultyName = new SelectList(db.Faculty, "facultyName", "facultyName", specialty.facultyName);
            return View(specialty);
        }

        // GET: Specialties/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialty.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Specialty specialty = db.Specialty.Find(id);
                db.deleteSpecialty(specialty.specialtyCode);
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
                return View("Error");
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
