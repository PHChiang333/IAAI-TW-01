using IAAI_TW_01.Models;
using IAAI_TW_01.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Controllers
{
    public class HomeController : Controller
    {
        DBModel db = new DBModel();


        public ActionResult Index()
        {
            List<News> newsList = new List<News>();

            newsList = db.News.Where(n => n.IsDeleted == false).OrderByDescending(p => p.UpdateAt).Take(4).ToList();

            var homeDto = new HomeDto
            {
                Newss = newsList
            };


            return View(homeDto);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}