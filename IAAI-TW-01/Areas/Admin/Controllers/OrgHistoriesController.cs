using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Models;
//Pagnation
using MvcPaging;
using IAAI_TW_01.Filter;

namespace IAAI_TW_01.Areas.Admin.Controllers
{


    [RouteArea("Admin")]
    [RoutePrefix("About/OrgHistories")]
    [PermissionFilter]
    public class OrgHistoriesController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/OrgHistories
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.OrgHistorys.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.OrgHistorys.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/OrgHistories/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgHistory orgHistory = db.OrgHistorys.Find(id);
            if (orgHistory == null)
            {
                return HttpNotFound();
            }
            return View(orgHistory);
        }

        // GET: Admin/OrgHistories/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/OrgHistories/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(OrgHistoryCreateDto orgHistoryCreateDto)
        {
            if (ModelState.IsValid)
            {
                var addOrgHistory = new OrgHistory
                {
                    Title = orgHistoryCreateDto.Title,
                    Content = orgHistoryCreateDto.Content,
                    IsTop = orgHistoryCreateDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.OrgHistorys.Add(addOrgHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orgHistoryCreateDto);
        }

        // GET: Admin/OrgHistories/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgHistory orgHistory = db.OrgHistorys.Find(id);
            if (orgHistory == null)
            {
                return HttpNotFound();
            }
            return View(orgHistory);
        }

        // POST: Admin/OrgHistories/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(OrgHistoryEditDto orgHistoryEditDto)
        {
            if (ModelState.IsValid)
            {
                var selOrgHistory = db.OrgHistorys.Find(orgHistoryEditDto.Id);

                selOrgHistory.Title = orgHistoryEditDto.Title;
                selOrgHistory.Content = orgHistoryEditDto.Content;
                selOrgHistory.IsTop = orgHistoryEditDto.IsTop;

                selOrgHistory.UpdateAt = DateTime.Now;


                //db.Entry(orgHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgHistoryEditDto);
        }

        // GET: Admin/OrgHistories/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgHistory orgHistory = db.OrgHistorys.Find(id);
            if (orgHistory == null)
            {
                return HttpNotFound();
            }
            return View(orgHistory);
        }

        // POST: Admin/OrgHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgHistory orgHistory = db.OrgHistorys.Find(id);
            db.OrgHistorys.Remove(orgHistory);
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
