using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
using MvcPaging;

namespace IAAI_TW_01.Areas.Admin.Controllers
{

    [RouteArea("Admin")]
    [RoutePrefix("About/Experts")]
    [PermissionFilter]
    public class ExpertsController : Controller
    {
        private DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Admin/Experts
        [Route("")]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.Experts.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.Experts.OrderByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));


            //return View(db.Experts.ToList());
        }

        // GET: Admin/Experts/Details/5
        [Route("Details/{id:int}")]
        public ActionResult Details(int? id)
        {

            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Expert expert = db.Experts.Find(id);
                if (expert == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(expert);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Experts/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: Admin/Experts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(ExpertCreateDto expertCreateDto)
        {
            //副檔名限制
            //圖片
            string[] allowedExts = { ".jpg", ".png" };
            //上傳路徑 
            string relativePath = "/Areas/Admin/Uploads/Expert/";
            string uploadPath = Server.MapPath("~" + relativePath);
            List<string> uploadResults = new List<string>();



            if (ModelState.IsValid)
            {
                try
                {
                    if (expertCreateDto.CoverFile == null || expertCreateDto.CoverFile.ContentLength == 0)
                    {
                        ViewBag.ErrorFile = "請確認檔案";
                        ViewBag.textDanger = "text-danger";
                        return View(expertCreateDto);
                    }
                    else
                    {
                        //確認有檔案
                        //找到副檔名
                        var fileExt = System.IO.Path.GetExtension(expertCreateDto.CoverFile.FileName).ToLower();

                        //非許可檔案類型，剔除
                        if (!allowedExts.Contains(fileExt))
                        {
                            ViewBag.ErrorFile = "檔案格式錯誤";
                            ViewBag.textDanger = "text-danger";
                            return View(expertCreateDto);
                        }
                        //確認檔案大小限制 :5MB
                        else if (expertCreateDto.CoverFile.ContentLength > 5 * 1024 * 1024)
                        {
                            ViewBag.ErrorFile = "檔案過大";
                            return View(expertCreateDto);
                        }


                        //確認檔案名稱(不含副檔名)
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(expertCreateDto.CoverFile.FileName);

                        string selFileRename = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                        string selFileFullPath = relativePath + selFileRename;
                        string selFileFullPathUpload = uploadPath + selFileRename;
                        expertCreateDto.CoverFile.SaveAs(selFileFullPathUpload);

                        //處理資料庫物件
                        var AddExpert = new Expert
                        {
                            Name = expertCreateDto.Name,
                            ServiceAt = expertCreateDto.ServiceAt,
                            CoverName = selFileRename,
                            CoverPath = selFileFullPath,
                            History = expertCreateDto.History,

                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            IsDeleted = false,
                            DeleteAt = null
                        };

                        db.Experts.Add(AddExpert);
                        db.SaveChanges();
                        return Redirect("/Admin/About/Experts");
                    }

                }
                catch (Exception ex)
                {
                    //TODO Admin/About/Expert/Create: 設定失敗Page
                    return Redirect("/Admin/About/Experts");
                }
            }

            return View(expertCreateDto);
        }



        // GET: Admin/Experts/Edit/5
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Expert expert = db.Experts.Find(id);
                if (expert == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(expert);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }


        // POST: Admin/Experts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, ExpertEditDto expertEditDto)
        {
            if (id != expertEditDto.Id)
            {
                return RedirectToAction("Index");
            }

            //判斷模型是否通過驗證
            if (ModelState.IsValid)
            {
                //副檔名限制
                //圖片
                string[] allowedExts = { ".jpg", ".png" };
                //上傳路徑 
                string relativePath = "/Areas/Admin/Uploads/Expert/";
                string uploadPath = Server.MapPath("~" + relativePath);
                List<string> uploadResults = new List<string>();

                try
                {
                    //模型驗證成功，更新資料庫
                    var expertData = db.Experts.Find(expertEditDto.Id);

                    expertData.Name = expertEditDto.Name;
                    expertData.ServiceAt = expertEditDto.ServiceAt;

                    //照片
                    if (expertEditDto.CoverFile == null || expertEditDto.CoverFile.ContentLength == 0)
                    {
                        //沒有上傳檔案，保留原本的檔案
                    }
                    else
                    {
                        //確認有檔案
                        //找到副檔名
                        var fileExt = System.IO.Path.GetExtension(expertEditDto.CoverFile.FileName).ToLower();

                        //非許可檔案類型，剔除
                        if (!allowedExts.Contains(fileExt))
                        {
                            ViewBag.ErrorFile = "檔案格式錯誤";
                            return View(expertEditDto);
                        }
                        //確認檔案大小限制 :5MB
                        else if (expertEditDto.CoverFile.ContentLength > 5 * 1024 * 1024)
                        {
                            ViewBag.ErrorFile = "檔案過大";
                            return View(expertEditDto);
                        }


                        //確認檔案名稱(不含副檔名)
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(expertEditDto.CoverFile.FileName);

                        string selFileRename = fileNameWithoutExt + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                        string selFileFullPath = relativePath + selFileRename;
                        string selFileFullPathUpload = uploadPath + selFileRename;
                        expertEditDto.CoverFile.SaveAs(selFileFullPathUpload);


                        expertData.CoverName = selFileRename;
                        expertData.CoverPath = selFileFullPath;
                    }

                    expertData.History = expertEditDto.History;

                    expertData.UpdateAt = DateTime.Now;

                    db.SaveChanges();
                    //重導至Index頁面
                    return Redirect("/Admin/About/Experts");

                }
                catch (Exception ex)
                {
                    //TODO 設定失敗Page
                    return RedirectToAction("Index");
                }
            }
            //驗證錯誤，回傳原本的表單
            return View(expertEditDto);
        }



        // GET: Admin/Experts/Delete/5
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int? id)
        {

            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Expert expert = db.Experts.Find(id);
                if (expert == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(expert);

            }
            catch (Exception ex)
            {
                //TODO 設定失敗Page
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Expert expert = db.Experts.Find(id);
                db.Experts.Remove(expert);
                db.SaveChanges();
                return Redirect("/Admin/About/Experts");

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
