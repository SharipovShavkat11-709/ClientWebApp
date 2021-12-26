using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseWork.Models;
using PagedList;

namespace CourseWork.Controllers
{
    public class RequestFstController : Controller
    {
        private studBDEntities db = new studBDEntities();
        // GET: RequestFst
        public ActionResult Index(int? page)
        {
            var search = db.Search1_1().ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(search.ToPagedList(pageNumber, pageSize));
        }
    }
}