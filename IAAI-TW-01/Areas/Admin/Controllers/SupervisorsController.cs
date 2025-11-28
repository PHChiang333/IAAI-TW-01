using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
//Pagnation
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("About/Supervisors")]
    [PermissionFilter]
    public class SupervisorsController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;



        [Route("")]
        // GET: Admin/Supervisors
        public ActionResult Index(int? page)
        {

            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Supervisors.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.Supervisors.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        // GET: Admin/About/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return Redirect("/Admin/About");
                }
                Supervisor supervisor = db.Supervisors.Find(id);
                if (supervisor == null)
                {
                    //return HttpNotFound();
                    return Redirect("/Admin/About");
                }
                return View(supervisor);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/About/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }



        // POST: Admin/About/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupervisorCreateDto supervisorCreateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    //驗證成功，新增資料
                    var AddSupvisor = new Supervisor();
                    AddSupvisor.PositionName = supervisorCreateDto.PositionName;
                    AddSupvisor.Name = supervisorCreateDto.Name;
                    AddSupvisor.Gender = supervisorCreateDto.Gender;
                    AddSupvisor.ServiceHistory = supervisorCreateDto.ServiceHistory;
                    AddSupvisor.Term = supervisorCreateDto.Term;
                    AddSupvisor.CreateAt = DateTime.Now;
                    AddSupvisor.UpdateAt = DateTime.Now;
                    AddSupvisor.IsDeleted = false;
                    AddSupvisor.DeleteAt = null;

                    db.Supervisors.Add(AddSupvisor);
                    db.SaveChanges();
                    return Redirect("/Admin/About");

                }
                catch (Exception ex)
                {
                    //TODO 設定失敗Page
                    return RedirectToAction("Index");
                }
            }
            //驗證失敗，返回原本的表單
            return View(supervisorCreateDto);
        }




        // GET: Admin/About/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return Redirect("/Admin/About");
                }
                Supervisor supervisor = db.Supervisors.Find(id);
                if (supervisor == null)
                {
                    //return HttpNotFound();
                    return Redirect("/Admin/About");
                }
                return View(supervisor);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }



        // POST: Admin/About/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, SupervisorEditDto supervisorEditDto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //db.Entry(supervisor).State = EntityState.Modified;
                    if (id != supervisorEditDto.Id)
                    {
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        return Redirect("/Admin/About");
                    }

                    var selSupervisor = db.Supervisors.Find(supervisorEditDto.Id);
                    if (selSupervisor == null)
                    {
                        //return HttpNotFound();
                        return Redirect("/Admin/About");
                    }
                    selSupervisor.PositionName = supervisorEditDto.PositionName;
                    selSupervisor.Name = supervisorEditDto.Name;
                    selSupervisor.Gender = supervisorEditDto.Gender;
                    selSupervisor.ServiceHistory = supervisorEditDto.ServiceHistory;
                    selSupervisor.Term = supervisorEditDto.Term;

                    selSupervisor.UpdateAt = DateTime.Now;

                    db.SaveChanges();
                    return Redirect("/Admin/About");

                }
                catch (Exception ex)
                {
                    //TODO 設定失敗Page
                    return RedirectToAction("Index");
                }
            }
            return View(supervisorEditDto);
        }



        // GET: Admin/About/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return Redirect("/Admin/About");
                }
                Supervisor supervisor = db.Supervisors.Find(id);
                if (supervisor == null)
                {
                    //return HttpNotFound();
                    return Redirect("/Admin/About");
                }
                return View(supervisor);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
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
