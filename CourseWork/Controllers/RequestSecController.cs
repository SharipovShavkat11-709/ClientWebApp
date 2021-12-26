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
using System.Data.Entity.Core;

namespace CourseWork.Controllers
{
    public class RequestSecController : Controller
    {
        public List<Search2_Result> searchRes;
        private studBDEntities db = new studBDEntities();
        // GET: RequestSec
        public ActionResult Index(int? page, string groupCode, int? avgSalary, string lastCode, int? lastSalary)
        {
            if (groupCode!=null && avgSalary.HasValue)
            {
                page = 1;
            }
            else
            {
                groupCode = lastCode;
                avgSalary = lastSalary;
            }

            ViewBag.CurrentGroup = groupCode;
            ViewBag.CurrentSalary = avgSalary;

            ViewBag.groupCode = new SelectList(db.Group, "groupCode", "groupCode");

            searchRes = db.Search2(groupCode, avgSalary).ToList();
            searchRes = searchRes.OrderBy(x => x.studentNumber).ToList();
 
            ViewBag.PagedList = searchRes.ToPagedList(1, 10);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(searchRes.ToPagedList(pageNumber, pageSize));
        }

    }
}