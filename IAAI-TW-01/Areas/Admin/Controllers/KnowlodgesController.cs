using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Resolvers;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    [PermissionFilter]
    public class KnowlodgesController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/Knowlodges
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Knowlodges.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.Knowlodges.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));

            //return View(db.Knowlodges.ToList());
        }

        // GET: Admin/Knowlodges/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Knowlodge knowlodge = db.Knowlodges.Find(id);
                if (knowlodge == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(knowlodge);
            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Knowlodges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Knowlodges/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KnowlodgeCreateDto knowlodgeCreateDto)
        {
            //副檔名限制
            string[] allowedExts = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };
            //上傳路徑 
            string relativePath = "/Areas/Admin/Uploads/Knowlodges/";
            string uploadPath = Server.MapPath("~" + relativePath);
            List<string> uploadResults = new List<string>();

            if (ModelState.IsValid)
            {

                if (knowlodgeCreateDto.File == null || knowlodgeCreateDto.File.ContentLength == 0)
                {
                    ViewBag.ErrorFile = "請確認檔案";
                    return View(knowlodgeCreateDto);
                }
                else
                {
                    //確認有檔案
                    //找到副檔名
                    var fileExt = System.IO.Path.GetExtension(knowlodgeCreateDto.File.FileName).ToLower();

                    //非許可檔案類型，剔除
                    if (!allowedExts.Contains(fileExt))
                    {
                        ViewBag.ErrorFile = "檔案格式錯誤";
                        return View(knowlodgeCreateDto);
                    }
                    //確認檔案大小限制 :5MB
                    else if (knowlodgeCreateDto.File.ContentLength > 5 * 1024 * 1024)
                    {
                        ViewBag.ErrorFile = "檔案過大";
                        return View(knowlodgeCreateDto);
                    }


                    //確認檔案名稱(不含副檔名)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(knowlodgeCreateDto.File.FileName);

                    string selFileRename = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                    string selFileFullPath = relativePath + selFileRename;
                    string selFileFullPathUpload = uploadPath + selFileRename;
                    knowlodgeCreateDto.File.SaveAs(selFileFullPathUpload);

                    //處理資料庫物件
                    var AddKnowlodge = new Knowlodge
                    {
                        Title = knowlodgeCreateDto.Title,
                        //FilePath = knowlodgeCreateDto.FilePath,
                        FileName = selFileRename,
                        FilePath = selFileFullPath,

                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false,
                        DeleteAt = null
                    };

                    db.Knowlodges.Add(AddKnowlodge);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(knowlodgeCreateDto);
        }

        // GET: Admin/Knowlodges/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Knowlodge knowlodge = db.Knowlodges.Find(id);
                if (knowlodge == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(knowlodge);
            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Knowlodges/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, KnowlodgeEditDto knowlodgeEditDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //db.Entry(knowlodge).State = EntityState.Modified;

                    if (db.Knowlodges.Find(id) == null)
                    {
                        return RedirectToAction("Index");
                    }

                    var selKnowlodge = db.Knowlodges.Find(id);



                    //副檔名限制
                    string[] allowedExts = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };
                    //上傳路徑
                    string relativePath = "/Areas/Admin/Uploads/Knowlodges/";
                    string uploadPath = Server.MapPath("~" + relativePath);
                    List<string> uploadResults = new List<string>();

                    if (knowlodgeEditDto.File == null || knowlodgeEditDto.File.ContentLength == 0)
                    {
                        ViewBag.ErrorFile = "請確認檔案";
                        return View(knowlodgeEditDto);
                    }
                    else
                    {
                        //確認有檔案
                        //找到副檔名
                        var fileExt = System.IO.Path.GetExtension(knowlodgeEditDto.File.FileName).ToLower();

                        //非許可檔案類型，剔除
                        if (!allowedExts.Contains(fileExt))
                        {
                            ViewBag.ErrorFile = "檔案格式錯誤";
                            return View(knowlodgeEditDto);
                        }
                        //確認檔案大小限制 :5MB
                        else if (knowlodgeEditDto.File.ContentLength > 5 * 1024 * 1024)
                        {
                            ViewBag.ErrorFile = "檔案過大";
                            return View(knowlodgeEditDto);
                        }

                        //確認檔案名稱(不含副檔名)
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(knowlodgeEditDto.File.FileName);

                        string selFileRename = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                        string selFileFullPath = relativePath + selFileRename;
                        string selFileFullPathUpload = uploadPath + selFileRename;
                        knowlodgeEditDto.File.SaveAs(selFileFullPathUpload);



                        //處理資料庫物件
                        selKnowlodge.Title = knowlodgeEditDto.Title;
                        selKnowlodge.FileName = selFileRename;
                        selKnowlodge.FilePath = selFileFullPath;
                        selKnowlodge.UpdateAt = DateTime.Now;

                        db.SaveChanges();


                        //刪除舊檔案
                        string oldFileFullPath = Server.MapPath("~" + selKnowlodge.FilePath);
                        if (System.IO.File.Exists(oldFileFullPath))
                        {
                            System.IO.File.Delete(oldFileFullPath);
                        }

                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    //TODO 設定失敗Page
                    return RedirectToAction("Index");
                }

            }
            return View(knowlodgeEditDto);
        }

        // GET: Admin/Knowlodges/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Knowlodge knowlodge = db.Knowlodges.Find(id);
                if (knowlodge == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(knowlodge);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Knowlodges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Knowlodge knowlodge = db.Knowlodges.Find(id);
                db.Knowlodges.Remove(knowlodge);
                db.SaveChanges();
                return RedirectToAction("Index");

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

