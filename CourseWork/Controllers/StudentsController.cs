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
    public class StudentsController : Controller
    {
        private studBDEntities db = new studBDEntities();

        // GET: Students
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var student = db.Student.OrderBy(Student => Student.studentNumber);
            return View(student.ToPagedList(pageNumber, pageSize));      
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.groupCode = new SelectList(db.Group, "groupCode", "groupCode");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentNumber,groupCode,fullName,fathersSalary,mothersSalary,numFamMembers")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Student.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error!!! The student with the same number already exist ");
                }
            }

            ViewBag.groupCode = new SelectList(db.Group, "groupCode", "groupCode", student.groupCode);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.groupCode = new SelectList(db.Group, "groupCode", "groupCode", student.groupCode);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentNumber,groupCode,fullName,fathersSalary,mothersSalary,numFamMembers")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.editStudent(student.studentNumber, student.groupCode, student.fullName, student.fathersSalary, student.mothersSalary, student.numFamMembers);
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
            ViewBag.groupCode = new SelectList(db.Group, "groupCode", "groupCode", student.groupCode);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Student.Find(id);
                db.deleteStudent(student.studentNumber);
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
