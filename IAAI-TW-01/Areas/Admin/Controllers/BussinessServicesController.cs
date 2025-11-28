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
    [RoutePrefix("Bussiness/BussinessServices")]
    [PermissionFilter]
    public class BussinessServicesController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/BussinessServices
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.BussinessServices.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.BussinessServices.OrderByDescending(p => p.IsTop).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/BussinessServices/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessService bussinessService = db.BussinessServices.Find(id);
            if (bussinessService == null)
            {
                return HttpNotFound();
            }
            return View(bussinessService);
        }

        // GET: Admin/BussinessServices/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BussinessServices/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(BussinessServicesDto bussinessServicesDto)
        {
            if (ModelState.IsValid)
            {
                var addBussinessService = new BussinessService
                {
                    Title = bussinessServicesDto.Title,
                    Content = bussinessServicesDto.Content,
                    IsTop = bussinessServicesDto.IsTop,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.BussinessServices.Add(addBussinessService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bussinessServicesDto);
        }

        // GET: Admin/BussinessServices/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessService bussinessService = db.BussinessServices.Find(id);
            if (bussinessService == null)
            {
                return HttpNotFound();
            }
            return View(bussinessService);
        }

        // POST: Admin/BussinessServices/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(BussinessServicesDto bussinessServicesDto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aboutUs).State = EntityState.Modified;

                var selBussinessService = db.BussinessServices.Find(bussinessServicesDto.Id);

                selBussinessService.Title = bussinessServicesDto.Title;
                selBussinessService.Content = bussinessServicesDto.Content;
                selBussinessService.IsTop = bussinessServicesDto.IsTop;

                selBussinessService.UpdateAt = DateTime.Now;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bussinessServicesDto);
        }

        // GET: Admin/BussinessServices/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinessService bussinessService = db.BussinessServices.Find(id);
            if (bussinessService == null)
            {
                return HttpNotFound();
            }
            return View(bussinessService);
        }

        // POST: Admin/BussinessServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            BussinessService bussinessService = db.BussinessServices.Find(id);
            db.BussinessServices.Remove(bussinessService);
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
