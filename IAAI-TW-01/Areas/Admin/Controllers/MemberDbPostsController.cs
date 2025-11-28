using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
using IAAI_TW_01.Models.Dto;
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{

    [RouteArea("Admin")]
    [RoutePrefix("Member/Member_db")]
    [PermissionFilter]
    public class MemberDbPostsController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/MemberDbPosts
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.MemberDbPosts.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.MemberDbPosts.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }


        // GET:Member/Member_db/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDbPost memberDbPost = db.MemberDbPosts.Find(id);
            if (memberDbPost == null)
            {
                return HttpNotFound();
            }

            var selPost = db.MemberDbPosts.Where(m => m.Id == id).FirstOrDefault();


            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            //var selRelys = db.MemberDbReplys.Where(m => m.MemberDbPostId == id).ToList();

            var selRelys = selPost.MemberDbReplys.OrderBy(p => p.CreateAt).ToPagedList(currentPageIndex, DefaultPageSize);
            //總資料筆數
            ViewBag.Count = selPost.MemberDbReplys.OrderBy(p => p.CreateAt).Count();


            var selMemberPostDetail = new Member_dbPostDetailDto
            {
                MemberDbPost = selPost,
                MemberDbReplys = selRelys
            };


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(selMemberPostDetail);
        }






        // GET: Admin/MemberDbPosts/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MemberDbPosts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create([Bind(Include = "Id,Title,Content,AuthorId,Author,CreateAt,UpdateAt,IsDeleted,DeleteAt")] MemberDbPost memberDbPost)
        {
            if (ModelState.IsValid)
            {
                db.MemberDbPosts.Add(memberDbPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberDbPost);
        }

        // GET: Admin/MemberDbPosts/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDbPost memberDbPost = db.MemberDbPosts.Find(id);
            if (memberDbPost == null)
            {
                return HttpNotFound();
            }
            return View(memberDbPost);
        }

        // POST: Admin/MemberDbPosts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AuthorId,Author,CreateAt,UpdateAt,IsDeleted,DeleteAt")] MemberDbPost memberDbPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberDbPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberDbPost);
        }

        // GET: Admin/MemberDbPosts/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDbPost memberDbPost = db.MemberDbPosts.Find(id);
            if (memberDbPost == null)
            {
                return HttpNotFound();
            }
            return View(memberDbPost);
        }

        // POST: Admin/MemberDbPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberDbPost memberDbPost = db.MemberDbPosts.Find(id);
            db.MemberDbPosts.Remove(memberDbPost);
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
