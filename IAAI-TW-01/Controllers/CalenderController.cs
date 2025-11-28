using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Controllers
{
    public class CalenderController : Controller
    {
        DBModel db = new DBModel();


        // GET: Calender
        public ActionResult Index()
        {
            return View();
        }
    }
}