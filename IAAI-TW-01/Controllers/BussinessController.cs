using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Controllers
{
    public class BussinessController : Controller
    {
        DBModel db = new DBModel();


        // GET: Bussiness
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BussinessServices()
        {
            return View(db.BussinessServices.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }

        public ActionResult BussinessTrainings()
        {
            return View(db.BussinessTrainings.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }

        public ActionResult BussinessConsults()
        {
            return View(db.BussinessConsults.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }

        public ActionResult BussinessSurveys()
        {
            return View(db.BussinessSurveys.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToList());
        }
    }
}