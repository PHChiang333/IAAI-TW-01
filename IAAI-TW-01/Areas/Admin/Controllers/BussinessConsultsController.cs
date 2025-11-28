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
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Bussiness/BussinessConsults")]
    [PermissionFilter]
    public class BussinessConsultsController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/BussinessConsults
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.BussinessConsults.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.BussinessConsults.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/BussinessConsults/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessConsult bussinessConsult = db.BussinessConsults.Find(id);
            if (bussinessConsult == null)
            {
                return HttpNotFound();
            }
            return View(bussinessConsult);
        }

        // GET: Admin/BussinessConsults/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BussinessConsults/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(BussinessConsultDto bussinessConsultDto)
        {
            if (ModelState.IsValid)
            {
                var addBussinessConsult = new BussinessConsult
                {
                    Title = bussinessConsultDto.Title,
                    Content = bussinessConsultDto.Content,
                    IsTop = bussinessConsultDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.BussinessConsults.Add(addBussinessConsult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bussinessConsultDto);
        }

        // GET: Admin/BussinessConsults/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessConsult bussinessConsult = db.BussinessConsults.Find(id);
            if (bussinessConsult == null)
            {
                return HttpNotFound();
            }
            return View(bussinessConsult);
        }

        // POST: Admin/BussinessConsults/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(BussinessConsultDto bussinessConsultDto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aboutUs).State = EntityState.Modified;

                var selBussinessConsult = db.BussinessConsults.Find(bussinessConsultDto.Id);

                selBussinessConsult.Title = bussinessConsultDto.Title;
                selBussinessConsult.Content = bussinessConsultDto.Content;
                selBussinessConsult.IsTop = bussinessConsultDto.IsTop;

                selBussinessConsult.UpdateAt = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bussinessConsultDto);
        }

        // GET: Admin/BussinessConsults/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessConsult bussinessConsult = db.BussinessConsults.Find(id);
            if (bussinessConsult == null)
            {
                return HttpNotFound();
            }
            return View(bussinessConsult);
        }

        // POST: Admin/BussinessConsults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            BussinessConsult bussinessConsult = db.BussinessConsults.Find(id);
            db.BussinessConsults.Remove(bussinessConsult);
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
