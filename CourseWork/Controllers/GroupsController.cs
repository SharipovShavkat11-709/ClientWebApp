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
    public class GroupsController : Controller
    {
        private studBDEntities db = new studBDEntities();

        // GET: Groups      
        public ActionResult Index(int? page)
        {           
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var group = db.Group.OrderBy(Group => Group.groupCode);
            return View(group.ToPagedList(pageNumber, pageSize));
        }

        // GET: Groups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.specialtyCode = new SelectList(db.Specialty, "specialtyCode", "specialtyCode");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]      
        public ActionResult Create([Bind(Include = "groupCode,specialtyCode,yearAdmition")] Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Group.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Error!!! The group with the same code already exist ");
                }
            }

            ViewBag.specialtyCode = new SelectList(db.Specialty, "specialtyCode", "specialtyCode", group.specialtyCode);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.specialtyCode = new SelectList(db.Specialty, "specialtyCode", "specialtyCode", group.specialtyCode);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "groupCode,specialtyCode,yearAdmition")] Group group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.editGroup(group.groupCode, group.specialtyCode, group.yearAdmition);
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
            ViewBag.specialtyCode = new SelectList(db.Specialty, "specialtyCode", "specialtyCode", group.specialtyCode);
            
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Group group = db.Group.Find(id);
                db.deleteGroup(group.groupCode);
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
