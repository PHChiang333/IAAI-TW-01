using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Filter;
using IAAI_TW_01.Models;
using IAAI_TW_01.Models.Dto;
using IAAI_TW_01.Models.Utility;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
//Pagnation
using MvcPaging;
using System.Web.UI;
using static Dropbox.Api.Team.GroupAccessType;

namespace IAAI_TW_01.Areas.Admin.Controllers
{
    public class AdminMembersController : Controller
    {
        private DBModel db = new DBModel();

                private const int DefaultPageSize = 10;

        // GET: Admin/AdminMembers
        [PermissionFilter]
        public ActionResult Index(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.AdminMembers.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.AdminMembers.OrderByDescending(p => p.IsTopAccess).ThenByDescending(p => p.UpdateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Admin/AdminMembers/Details/5
        [PermissionFilter]
        public ActionResult Details(int? id)
        {
            //組tree.js data字串
            ViewBag.TreeView = GetTreeView();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMember adminMember = db.AdminMembers.Find(id);
            if (adminMember == null)
            {
                return HttpNotFound();
            }
            return View(adminMember);
        }

        // GET: Admin/AdminMembers/Create
        [PermissionFilter]
        public ActionResult Create()
        {
            //組tree.js data字串
            ViewBag.TreeView = GetTreeView();


            return View();
        }

        // POST: Admin/AdminMembers/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionFilter]
        public ActionResult Create(AdminMemberCreateDto adminMemberCreateDto)
        {
            //組tree.js data字串
            ViewBag.TreeView = GetTreeView();

            try
            {

                // 驗證資料(模型驗證)
                if (!ModelState.IsValid)
                {
                    // 資料異常
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "註冊失敗，請確認資料";
                    return View(adminMemberCreateDto);
                }


                // 檢查是否已經註冊過

                if (db.AdminMembers.Where(m => m.IsDeleted == false).Any(m => m.Account == adminMemberCreateDto.Account))
                {
                    // 帳號重複(已存在)
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "帳號已存在，請確認資料";
                    return View(adminMemberCreateDto);
                }
                else
                {

                    //正式註冊流程

                    //處理密碼
                    //原始密碼
                    var selPassword = adminMemberCreateDto.Password;
                    //產生密碼鹽
                    var salt = Utility.CreateSalt();
                    //密碼加密
                    var hash = Utility.GenerateHashWithSalt(selPassword, salt);

                    AdminMember addAdminMember = new AdminMember
                    {
                        Account = adminMemberCreateDto.Account,
                        Password = selPassword,
                        PasswordSalt = salt,
                        PasswordHash = hash,
                        Permission = adminMemberCreateDto.Permission,
                        //IsAdmin = adminMemberCreateDto.IsAdmin,
                        IsTopAccess = false, //最高權限預設為false
                        NewPermission = adminMemberCreateDto.NewPermission,

                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false,
                        DeleteAt = null

                    };

                    db.AdminMembers.Add(addAdminMember);
                    db.SaveChanges();


                    // 註冊成功，跳轉到登入頁面
                    // 註冊後不自動登入，要求重新登入
                    ViewBag.LoginMsg = "註冊成功，請登入";
                    ViewBag.LoginMsgClass = "text-success";
                    return RedirectToAction("Login");

                }
            }
            catch (Exception ex)
            {
                // 處理異常
                ViewBag.ErrorMessageClass = "text-danger";
                ViewBag.ErrorMessage = "註冊失敗，請稍後再試。請聯繫系統管理員。";
                //ModelState.AddModelError("", "Error sending email: " + ex.Message);
                return View(adminMemberCreateDto);
            }


        }

        // GET: Admin/AdminMembers/Edit/5
        [PermissionFilter]
        public ActionResult Edit(int? id)
        {
            //組tree.js data字串
            ViewBag.TreeView = GetTreeView();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMember adminMember = db.AdminMembers.Find(id);
            if (adminMember == null)
            {
                return HttpNotFound();
            }
            return View(adminMember);
        }

        // POST: Admin/AdminMembers/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionFilter]
        public ActionResult Edit(AdminMemberEditDto adminMemberEditDto)
        {
            //組tree.js data字串
            ViewBag.TreeView = GetTreeView();

            try
            {



                // 驗證資料(模型驗證)
                if (!ModelState.IsValid)
                {
                    // 資料異常
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "註冊失敗，請確認資料";
                    return View(adminMemberEditDto);
                }


                // 檢查是否已經不存在

                if (!db.AdminMembers.Where(m => m.IsDeleted == false).Any(m => m.Id == adminMemberEditDto.Id))
                {
                    // 帳號不存在
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "帳號不存在，請確認資料";
                    return View(adminMemberEditDto);
                }

                else
                {
                    //找出帳號
                    var selAdminMember = db.AdminMembers.Where(m => m.IsDeleted == false).FirstOrDefault(m => m.Id == adminMemberEditDto.Id);

                    //確認密碼是否正確

                    //dto密碼
                    var dtoPassword = adminMemberEditDto.Password;
                    //密碼鹽
                    var salt = selAdminMember.PasswordSalt;
                    //密碼hash
                    var dtoHash = Utility.GenerateHashWithSalt(dtoPassword, salt);

                    //比對密碼
                    if (dtoHash != selAdminMember.PasswordHash)
                    {
                        // 密碼錯誤
                        ViewBag.ErrorMessageClass = "text-danger";
                        ViewBag.ErrorMessage = "密碼錯誤，請確認資料";
                        return View(adminMemberEditDto);
                    }

                    //正式編輯流程


                    selAdminMember.Permission = adminMemberEditDto.Permission;
                    selAdminMember.IsTopAccess = adminMemberEditDto.IsTopAccess;
                    selAdminMember.NewPermission=adminMemberEditDto.NewPermission;

                    selAdminMember.UpdateAt = DateTime.Now;


                    db.SaveChanges();


                    // 註冊成功，跳轉到登入頁面
                    // 註冊後不自動登入，要求重新登入
                    ViewBag.LoginMsg = "編輯成功";
                    ViewBag.LoginMsgClass = "text-success";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // 處理異常
                ViewBag.ErrorMessageClass = "text-danger";
                ViewBag.ErrorMessage = "註冊失敗，請稍後再試。請聯繫系統管理員。";
                //ModelState.AddModelError("", "Error sending email: " + ex.Message);
                return View(adminMemberEditDto);
            }

        }

        // GET: Admin/AdminMembers/Delete/5
        [PermissionFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMember adminMember = db.AdminMembers.Find(id);
            if (adminMember == null)
            {
                return HttpNotFound();
            }
            return View(adminMember);
        }

        // POST: Admin/AdminMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminMember adminMember = db.AdminMembers.Find(id);
            db.AdminMembers.Remove(adminMember);
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


        //Get: Admin/AdminMembers/Login
        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminMemberLoginDto adminMemberLoginDto)
        {
            if (ModelState.IsValid)
            {
                //驗證帳號密碼
                AdminMember adminMember = ValidateUser(adminMemberLoginDto.Account, adminMemberLoginDto.Password);
                //登入失敗
                if (adminMember == null)
                {
                    ViewBag.ErrorMessage = "登入失敗";
                    return View(adminMemberLoginDto);
                }

                //登入成功，表單驗證儲存使用者資訊



                //string userData = JsonConvert.SerializeObject(adminMember);

                var selMemberData = new
                {
                    Id = adminMember.Id,
                    Account = adminMember.Account,
                    //Name = adminMember.MemberInfos.Where(m => m.MemberId == adminMember.Id).Select(m => m.Name).FirstOrDefault(),
                    IsTopAccess = adminMember.IsTopAccess,
                };
                string userData = JsonConvert.SerializeObject(selMemberData);

                Utility.SetAuthenTicket(userData, adminMember.Account, Response);
                return RedirectToAction("Index","Home");

            }

            return View(adminMemberLoginDto);
        }

        //登出Action
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除Cache，避免登出後按上一頁還會顯示Cache頁面
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();


            return RedirectToAction("Login");
        }


        /// <summary>
        /// 驗證使用者
        /// </summary>
        /// <param name="account">輸入帳號</param>
        /// <param name="password">輸入密碼</param>
        /// <returns></returns>
        private AdminMember ValidateUser(string account, string password)
        {
            //確認帳號是否存在
            AdminMember adminMember = db.AdminMembers.FirstOrDefault(m => m.Account == account);
            if (adminMember == null)
            {
                return null;
            }
            //確認密碼是否正確
            //資料庫資料
            //雜湊後的Pw
            string dbPasswordHash = adminMember.PasswordHash;
            string salt = adminMember.PasswordSalt;
            //產生雜湊密碼
            var selPasswordHash = Utility.GenerateHashWithSalt(password, salt);
            if (selPasswordHash != dbPasswordHash)
            {
                return null;
            }
            return adminMember;
        }


        //權限樹
        private string GetTreeView()
        {

            StringBuilder sb = new StringBuilder();
            //撈所有權限資料
            var permissions = db.Permissions.Where(s => s.ParentId == null).ToList();

            sb.Append("[");
            sb.Append(GetSubTree(permissions));
            sb.Append("]");

            return sb.ToString();
        }


        /// <summary>
        /// 處理遞迴
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        private string GetSubTree(ICollection<Permission> permissions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in permissions)
            {
                sb.Append("{");
                sb.Append($"\"id\": \"{item.Code}\",");
                sb.Append($"\"text\": \"{item.Subject}\"");
                if (item.Permissions.Count() > 0)
                {
                    sb.Append($",\r\n        \"children\": ");
                    sb.Append("[");
                    sb.Append(GetSubTree(item.Permissions));
                    sb.Append("]");
                }
                sb.Append("},");
            }
            return sb.ToString();
        }














    }
}
