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
    [RoutePrefix("Bussiness/BussinessTrainings")]
    [PermissionFilter]
    public class BussinessTrainingsController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/BussinessTrainings
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.BussinessTrainings.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.BussinessTrainings.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/BussinessTrainings/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessTraining bussinessTraining = db.BussinessTrainings.Find(id);
            if (bussinessTraining == null)
            {
                return HttpNotFound();
            }
            return View(bussinessTraining);
        }

        // GET: Admin/BussinessTrainings/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BussinessTrainings/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(BussinessTrainingDto bussinessTrainingDto)
        {
            if (ModelState.IsValid)
            {
                var addBussinessTraining = new BussinessTraining
                {
                    Title = bussinessTrainingDto.Title,
                    Content = bussinessTrainingDto.Content,
                    IsTop = bussinessTrainingDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.BussinessTrainings.Add(addBussinessTraining);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bussinessTrainingDto);
        }

        // GET: Admin/BussinessTrainings/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessTraining bussinessTraining = db.BussinessTrainings.Find(id);
            if (bussinessTraining == null)
            {
                return HttpNotFound();
            }
            return View(bussinessTraining);
        }

        // POST: Admin/BussinessTrainings/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(BussinessTrainingDto bussinessTrainingDto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aboutUs).State = EntityState.Modified;

                var selBussinessTraining = db.BussinessTrainings.Find(bussinessTrainingDto.Id);

                selBussinessTraining.Title = bussinessTrainingDto.Title;
                selBussinessTraining.Content = bussinessTrainingDto.Content;
                selBussinessTraining.IsTop = bussinessTrainingDto.IsTop;

                selBussinessTraining.UpdateAt = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bussinessTrainingDto);
        }

        // GET: Admin/BussinessTrainings/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessTraining bussinessTraining = db.BussinessTrainings.Find(id);
            if (bussinessTraining == null)
            {
                return HttpNotFound();
            }
            return View(bussinessTraining);
        }

        // POST: Admin/BussinessTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            BussinessTraining bussinessTraining = db.BussinessTrainings.Find(id);
            db.BussinessTrainings.Remove(bussinessTraining);
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
