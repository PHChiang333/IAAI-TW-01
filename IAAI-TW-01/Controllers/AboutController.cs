using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Controllers
{
    public class AboutController : Controller
    {
        
        DBModel db = new DBModel();

        // GET: About
        [Route("About/AboutUs")]
        public ActionResult Index()
        {
            return View(db.AboutUs.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }


        // GET: Expert
        public ActionResult Experts()
        {
           
            return View(db.Experts.OrderByDescending(p => p.UpdateAt).ToList());
        }

        // GET: About
        public ActionResult Supervisors()
        {
            return View(db.Supervisors.OrderByDescending(p => p.UpdateAt).ToList());
        }

        // GET: About
        public ActionResult OrgHistories()
        {
            return View(db.OrgHistorys.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }




    }
}