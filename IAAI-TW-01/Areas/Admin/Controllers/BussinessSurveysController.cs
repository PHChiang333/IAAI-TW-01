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
using IAAI_TW_01.Models;
using MvcPaging;
using IAAI_TW_01.Filter;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Bussiness/BussinessSurveys")]
    [PermissionFilter]
    public class BussinessSurveysController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;


        // GET: Admin/BussinessSurveys
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.BussinessSurveys.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.BussinessSurveys.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/BussinessSurveys/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessSurvey bussinessSurvey = db.BussinessSurveys.Find(id);
            if (bussinessSurvey == null)
            {
                return HttpNotFound();
            }
            return View(bussinessSurvey);
        }

        // GET: Admin/BussinessSurveys/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BussinessSurveys/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(BussinessSurveyDto bussinessSurveyDto)
        {
            if (ModelState.IsValid)
            {
                var addBussinessSurvey = new BussinessSurvey
                {
                    Title = bussinessSurveyDto.Title,
                    Content = bussinessSurveyDto.Content,
                    IsTop = bussinessSurveyDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.BussinessSurveys.Add(addBussinessSurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bussinessSurveyDto);
        }

        // GET: Admin/BussinessSurveys/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessSurvey bussinessSurvey = db.BussinessSurveys.Find(id);
            if (bussinessSurvey == null)
            {
                return HttpNotFound();
            }
            return View(bussinessSurvey);
        }

        // POST: Admin/BussinessSurveys/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(BussinessSurveyDto bussinessSurveyDto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aboutUs).State = EntityState.Modified;

                var selBussinessSurvey = db.BussinessSurveys.Find(bussinessSurveyDto.Id);

                selBussinessSurvey.Title = bussinessSurveyDto.Title;
                selBussinessSurvey.Content = bussinessSurveyDto.Content;
                selBussinessSurvey.IsTop = bussinessSurveyDto.IsTop;

                selBussinessSurvey.UpdateAt = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bussinessSurveyDto);
        }

        // GET: Admin/BussinessSurveys/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessSurvey bussinessSurvey = db.BussinessSurveys.Find(id);
            if (bussinessSurvey == null)
            {
                return HttpNotFound();
            }
            return View(bussinessSurvey);
        }

        // POST: Admin/BussinessSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            BussinessSurvey bussinessSurvey = db.BussinessSurveys.Find(id);
            db.BussinessSurveys.Remove(bussinessSurvey);
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
