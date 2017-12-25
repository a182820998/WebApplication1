using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataBase;
using WebApplication1.Models.TestData;

namespace WebApplication1.Controllers.React
{
    public class ReactController : Controller
    {
        private StoreContext _db = new StoreContext();

        // GET: React
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReactJson()
        {
            var student = _db.Students.Join(
                              _db.Grades,
                              s => s.Id,
                              g => g.Id,
                              (s, g) => new
            {
                s.Name,
                g.Math
            }
                          );
            return Json(student, JsonRequestBehavior.AllowGet);
        }
    }
}