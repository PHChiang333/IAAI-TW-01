using IAAI_TW_01.Models;
using IAAI_TW_01.Models.Dto;
using IAAI_TW_01.Models.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

using MvcPaging;
using System.Net;
using System.Web.UI;
using IAAI_TW_01.Areas.Admin.Data;
using IAAI_TW_01.Models.Auth;

namespace IAAI_TW_01.Controllers
{
    public class MemberController : Controller
    {
        DBModel db = new DBModel();

        private const int DefaultPageSize = 10;

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        // GET: Member/Member_login
        public ActionResult Member_login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Member_login(LoginDto loginDto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //驗證帳號密碼
                    Member member = Utility.ValidateUser(db, loginDto.Account, loginDto.Password);
                    //登入失敗
                    if (member == null)
                    {
                        ViewBag.ErrorMessage = "登入失敗";
                        ViewBag.ErrorMessageClass = "text-danger";
                        return View();
                        //return RedirectToAction("Member_login");
                    }

                    //登入成功，表單驗證儲存使用者資訊

                    //這一這邊會連同FK一起序列化，造成cookie超過長度而無法寫入!
                    //string userData = JsonConvert.SerializeObject(member);

                    var selMemberData = new
                    {
                        Id = member.Id,
                        Account = member.Account,
                        Name = member.MemberInfos.Where(m => m.MemberId == member.Id).Select(m => m.Name).FirstOrDefault(),
                        IsAdmin = member.IsAdmin,
                    };

                    string userData = JsonConvert.SerializeObject(selMemberData);

                    //Utility.SetAuthenTicket(userData, member.Account);
                    Utility.SetAuthenTicket(userData, member.Id.ToString(), Response);
                    return RedirectToAction("Member_db");
                }
            }
            catch (Exception ex)
            {
                //登入失敗
                ViewBag.ErrorMessage = "登入失敗，請稍後再試。請聯繫管理員。";
                ViewBag.ErrorMessageClass = "text-danger";
                return View();
                //return RedirectToAction("Member_login");
            }

            ViewBag.ErrorMessage = "登入失敗，請稍後再試。請聯繫管理員。";
            ViewBag.ErrorMessageClass = "text-danger";
            return View();
            //return RedirectToAction("Member_login");
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


            return RedirectToAction("Member_login");
        }





        // GET: Member/Member_reg
        public ActionResult Member_reg()
        {
            //管理員登入再放
            //組tree.js data字串
            //ViewBag.TreeView = GetTreeView();

            return View();
        }

        [HttpPost]
        public ActionResult Member_reg(MemberRegDto memberRegDto)
        {
            // 這裡可以添加註冊邏輯，例如將用戶信息存儲到數據庫中
            // 例如：db.Users.Add(new User { Name = name, Email = email, Password = password });
            // db.SaveChanges();

            try
            {
                // 驗證資料(模型驗證)
                if (!ModelState.IsValid)
                {
                    // 資料異常
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "註冊失敗，請確認資料";
                    return View(memberRegDto);
                }


                // 檢查是否已經註冊過

                if (db.Members.Where(m => m.IsDeleted == false).Any(m => m.Account == memberRegDto.Account))
                {
                    // 帳號重複(已存在)
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "帳號已存在，請確認資料";
                    return View(memberRegDto);
                }
                else
                {

                    //正式註冊流程

                    //處理密碼
                    //原始密碼
                    var selPassword = memberRegDto.Password;
                    //產生密碼鹽
                    var salt = Utility.CreateSalt();
                    //密碼加密
                    var hash = Utility.GenerateHashWithSalt(selPassword, salt);

                    Member addMember = new Member
                    {
                        Account = memberRegDto.Account,
                        Password = selPassword,
                        PasswordSalt = salt,
                        PasswordHash = hash,
                        Permission = memberRegDto.Permission,
                        //IsAdmin = memberRegDto.IsAdmin,
                        IsAdmin = false, //會員預設
                        NewPermission = memberRegDto.NewPermission,

                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false,
                        DeleteAt = null

                    };

                    db.Members.Add(addMember);
                    db.SaveChanges();


                    MemberInfo addMemberInfo = new MemberInfo
                    {
                        MemberId = addMember.Id,  //FK

                        Name = memberRegDto.memberRegInfo.Name,
                        Gender = memberRegDto.memberRegInfo.Gender,
                        Birth = memberRegDto.memberRegInfo.Birth,
                        MemberType = memberRegDto.memberRegInfo.MemberType,
                        ContactAddress = memberRegDto.memberRegInfo.ContactAddress,
                        Email = memberRegDto.memberRegInfo.Email,
                        IsInternationalMember = memberRegDto.memberRegInfo.IsInternationalMember,
                        ServiceAt = memberRegDto.memberRegInfo.ServiceAt,
                        PositionName = memberRegDto.memberRegInfo.PositionName,
                        TopLevelEducation = memberRegDto.memberRegInfo.TopLevelEducation,

                        RelativeServicedTime = memberRegDto.memberRegInfo.RelativeServicedTime,
                        RelativeServicedTimeYear = memberRegDto.memberRegInfo.RelativeServicedTimeYear,
                        RelativeServicedTimeMonth = memberRegDto.memberRegInfo.RelativeServicedTimeMonth,

                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false,
                        DeleteAt = null
                    };

                    db.MemberInfos.Add(addMemberInfo);
                    db.SaveChanges();

                    foreach (MemberRegService item in memberRegDto.memberRegServices)
                    {

                        MemberService addMemberService = new MemberService
                        {
                            MemberId = addMember.Id,  //FK

                            ServiceAt = item.ServiceAt,
                            PositionName = item.PositionName,
                            ServicedTimeStart = item.ServicedTimeStart,
                            ServicedTimeEnd = item.ServicedTimeEnd,
                            ServicedTimePeriod = item.ServicedTimePeriod,

                            ServicedTimeStartYear = item.ServicedTimeStartYear,
                            ServicedTimeStartMonth = item.ServicedTimeStartMonth,
                            ServicedTimeEndYear = item.ServicedTimeEndYear,
                            ServicedTimeEndMonth = item.ServicedTimeEndMonth,

                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            IsDeleted = false,
                            DeleteAt = null
                        };

                        db.MemberServices.Add(addMemberService);
                        db.SaveChanges();
                    }


                    // 註冊成功，跳轉到登入頁面
                    // 註冊後不自動登入，要求重新登入
                    ViewBag.LoginMsg = "註冊成功，請登入";
                    ViewBag.LoginMsgClass = "text-success";
                    return RedirectToAction("Member_login");

                }
            }
            catch (Exception ex)
            {
                // 處理異常
                ViewBag.ErrorMessageClass = "text-danger";
                ViewBag.ErrorMessage = "註冊失敗，請稍後再試。請聯繫管理員。";
                //ModelState.AddModelError("", "Error sending email: " + ex.Message);
                return View(memberRegDto);
            }


        }



        // GET: Member/Member_dl
        [Authorize]  //驗證登入狀態
        public ActionResult Member_dl()
        {
            return View();
        }




















        // GET: Member/Member_db  
        // 討論區Discussion Board
        [Authorize]  //驗證登入狀態
        public ActionResult Member_db(int? page)
        {
            //現在第幾頁(當前頁面的索引值)
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            //總資料筆數
            ViewBag.Count = db.MemberDbPosts.Count();


            //返回結果.ToPageList(現在第幾頁,一頁幾筆)
            return View(db.MemberDbPosts.OrderByDescending(p => p.CreateAt).ToPagedList(currentPageIndex, DefaultPageSize));
        }


        // GET:Member/Member_db/Details/5
        [Authorize]  //驗證登入狀態
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


        // GET: Admin/BussinessTrainings/Create
        //[Route("Create")]
        [Authorize]  //驗證登入狀態
        public ActionResult Member_dbPostCreate()
        {
            return View();
        }

        // POST: Admin/BussinessTrainings/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("Create")]
        [Authorize]  //驗證登入狀態
        public ActionResult Member_dbPostCreate(Member_dbPostCreateDto member_DbPostCreateDto)
        {
            int selUserId = 0;
            string selUserName = string.Empty;


            // 從目前登入使用者取得驗證票
            if (User.Identity.IsAuthenticated)
            {
                // 取得加密的認證 Cookie
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    // 解密 cookie
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (ticket != null)
                    {
                        // 取出儲存在 UserData 的 JSON 字串
                        string userData = ticket.UserData;

                        // 反序列化回原本的物件（要跟當初 Serialize 的 class 結構一致）
                        AuthTicketUserDataDto member = JsonConvert.DeserializeObject<AuthTicketUserDataDto>(userData);

                        selUserId = member.Id; // 就是你當初傳入的 userId 或帳號
                                               // 取得使用者名稱
                        selUserName = member.Name;
                    }
                }
            }
            else
            {
                return RedirectToAction("Member_login");
            }


            if (ModelState.IsValid)
            {

                var addBussinessTraining = new MemberDbPost
                {
                    Title = member_DbPostCreateDto.Title,
                    Content = member_DbPostCreateDto.Content,
                    AuthorId = selUserId,
                    Author = selUserName,

                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.MemberDbPosts.Add(addBussinessTraining);
                db.SaveChanges();
                return RedirectToAction("Member_db");
            }

            return View(member_DbPostCreateDto);
        }

        // GET: Admin/BussinessTrainings/Create
        //[Route("Create")]
        [Authorize]  //驗證登入狀態
        public ActionResult Member_dbPostReplyCreate(int? postId)
        {
            //var selPost = db.MemberDbPosts.Where(p =>p.Id == postId).FirstOrDefault();

            //return View(selPost);


            if (postId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDbPost memberDbPost = db.MemberDbPosts.Find(postId);
            if (memberDbPost == null)
            {
                return HttpNotFound();
            }
            return View(memberDbPost);
        }

        // POST: Admin/BussinessTrainings/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("Create")]
        [Authorize]  //驗證登入狀態
        public ActionResult Member_dbPostReplyCreate(Member_dbPostReplyCreateDto member_DbPostReplyCreateDto)
        {
            int selUserId = 0;
            string selUserName = string.Empty;


            // 從目前登入使用者取得驗證票
            if (User.Identity.IsAuthenticated)
            {
                // 取得加密的認證 Cookie
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    // 解密 cookie
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (ticket != null)
                    {
                        // 取出儲存在 UserData 的 JSON 字串
                        string userData = ticket.UserData;

                        // 反序列化回原本的物件（要跟當初 Serialize 的 class 結構一致）
                        AuthTicketUserDataDto member = JsonConvert.DeserializeObject<AuthTicketUserDataDto>(userData);

                        selUserId = member.Id; // 就是你當初傳入的 userId 或帳號
                                               // 取得使用者名稱
                        selUserName = member.Name;
                    }
                }
            }
            else
            {
                return RedirectToAction("Member_login");
            }


            if (ModelState.IsValid)
            {


                var addMemberDbReply = new MemberDbReply
                {
                    Content = member_DbPostReplyCreateDto.Content,
                    AuthorId = selUserId,
                    Author = selUserName,
                    MemberDbPostId = member_DbPostReplyCreateDto.MemberDbPostId,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false
                };


                db.MemberDbReplys.Add(addMemberDbReply);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = member_DbPostReplyCreateDto.MemberDbPostId });
            }

            return View(member_DbPostReplyCreateDto);
        }




















    }
}