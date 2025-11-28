using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Pagnation
using MvcPaging;

namespace IAAI_TW_01.Controllers
{
    public class NewsController : Controller
    {
        DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        //// GET: News
        //public ActionResult Index()
        //{

        //    return View(db.News.ToList());
        //}

        // GET: News
        public ActionResult Index(int? page,string keyword)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            //如果沒有關鍵字，則顯示所有資料
            if (string.IsNullOrEmpty(keyword))
            {
                ViewBag.Count = db.News.Count();
                //返回結果.ToPageList(現在第幾頁,一頁幾筆)
                return View(db.News.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
            }

            //總資料筆數
            ViewBag.Count = db.News.Where(n => n.Title.Contains(keyword)).Count();

            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.News.Where(n => n.Title.Contains(keyword)).OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));

            //return View(db.News.ToList());
        }

        //// GET: News
        //public ActionResult Search(int? page,string keywords)
        //{
        //    //現在第幾頁(當前頁面的索引值)
        //    int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
        //    //總資料筆數
        //    ViewBag.Count = db.News.Count();


        //    //返回結果.ToPageList(現在第幾頁,一頁幾筆)
        //    return View(db.News.Where(n => n.Title.Contains(keywords)).OrderByDescending(p => p.CreateAt).ToPagedList(currentPageIndex, DefaultPageSize));

        //    //return View(db.News.ToList());
        //}






        [Route("News/{id:int}")]
        public ActionResult Detail(int id)
        {

            return View(db.News.Where(p => p.Id == id).FirstOrDefault());
        }


    }
}