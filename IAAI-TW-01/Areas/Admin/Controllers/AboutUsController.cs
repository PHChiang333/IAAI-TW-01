using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
//Pagnation
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("About/AboutUs")]
    [PermissionFilter]
    public class AboutUsController : Controller
    {
        private DBModel db = new DBModel();


        private const int DefaultPageSize = 10;

        // GET: Admin/AboutUs
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.AboutUs.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.AboutUs.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/AboutUs/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // GET: Admin/AboutUs/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AboutUs/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(AboutUsCreateDto aboutUsCreateDto)
        {
            if (ModelState.IsValid)
            {
                var addAboutUs = new AboutUs
                {
                    Title = aboutUsCreateDto.Title,
                    Content = aboutUsCreateDto.Content,
                    IsTop = aboutUsCreateDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.AboutUs.Add(addAboutUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutUsCreateDto);
        }

        // GET: Admin/AboutUs/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: Admin/AboutUs/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(AboutUsEditDto aboutUsEditDto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aboutUs).State = EntityState.Modified;

                var selAboutUs = db.AboutUs.Find(aboutUsEditDto.Id);

                selAboutUs.Title = aboutUsEditDto.Title;
                selAboutUs.Content = aboutUsEditDto.Content;
                selAboutUs.IsTop = aboutUsEditDto.IsTop;

                selAboutUs.UpdateAt = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutUsEditDto);
        }

        // GET: Admin/AboutUs/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: Admin/AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutUs aboutUs = db.AboutUs.Find(id);
            db.AboutUs.Remove(aboutUs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
