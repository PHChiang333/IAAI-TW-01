using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Pagnation
using MvcPaging;
using System.Web.UI;

namespace IAAI_TW_01.Controllers
{
    public class KnowledgeController : Controller
    {
        DBModel db = new DBModel();

        private const int DefaultPageSize = 10;


        // GET: Knowledge
        [Route("Knowlodges")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Knowlodges.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.Knowlodges.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }
    }
}