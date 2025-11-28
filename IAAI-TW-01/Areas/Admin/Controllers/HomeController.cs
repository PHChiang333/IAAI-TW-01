using IAAI_TW_01.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [PermissionFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}