using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IAAI_TW_01.Filter
{
    public class PermissionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DBModel db = new DBModel();

            //判斷是否有登入(表單驗證有通過)
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.Side = "";
                filterContext.Result = new RedirectResult("~/Admin/AdminMembers/Login");

                return;
            }

            // 取得帳號
            string account = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name;

            AdminMember adminMember = db.AdminMembers.FirstOrDefault(x => x.Account == account);
            //判斷是否有此使用者，若無則登出
            if (adminMember == null)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectResult("~/Admin/AdminMembers/Login");
                return;
            }

            //取得使用者的權限
            string userPermissions = adminMember.NewPermission;
            //取得符合使用者的資料庫權限資料
            var permissions = db.Permissions.Where(x => userPermissions.Contains(x.Code)).ToList();

            //判斷使用者是否有當下的controller權限
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //若無則登出
            if (!permissions.Any(x => x.ControllerName == controllerName))
            {
                FormsAuthentication.SignOut();
                filterContext.Controller.TempData["ErrorMessage"] = "您沒有此頁面的存取權限，請重新登入。";
                filterContext.Controller.TempData["ErrorMessageClass"] = "alert alert-danger";
                filterContext.Result = new RedirectResult("~/Admin/AdminMembers/Login");
                return;
            }


            //取得使用者的側邊menu
            StringBuilder sb = new StringBuilder();
            //開始ul
            sb.Append("<ul class=\"nav\">");
            //取得第一層
            var firstPermissions = permissions.Where(x => x.ParentId == null).ToList();
            foreach (var permission in firstPermissions)
            {
                string current = "";
                string open = "";
                //原本的
                if (permission.ControllerName.Contains(controllerName))
                {
                    current = "current";
                    open = "open";
                }

                //安全判斷，防止ControllerName==null報錯
                //if (!string.IsNullOrEmpty(permission.ControllerName) && permission.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase))
                //{
                //    current = "current";
                //    open = "open";
                //}


                if (!permission.Permissions.Any())
                {
                    //沒子層的
                    sb.Append($"<li class =\"{current}\">");
                    sb.Append($"<a href=\"{permission.Url}\">");
                    sb.Append($"<i class=\"glyphicon glyphicon-calendar\"></i>");
                    sb.Append($"{permission.Subject}");
                    sb.Append("</a>");
                    sb.Append("</li>");

                }
                else
                {
                    //有子層的
                    sb.Append($"<li class=\"submenu {open}\">");
                    sb.Append("<a href=\"#\">");
                    sb.Append("<i class=\"glyphicon glyphicon-list\"></i>");
                    sb.Append($"{permission.Subject}");
                    sb.Append("<span class=\"caret pull-right\"></span>");
                    sb.Append("</a>");

                    //使用遞迴
                    sb.Append(GetSub(permission.Permissions, userPermissions));
                    sb.Append("</li>");
                }
            }

            //關閉ul
            sb.Append("</ul>");
            filterContext.Controller.ViewBag.Side = sb.ToString();
        }

        /// <summary>
        /// SideMenu遞迴取得子層
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="userPermissions"></param>
        /// <returns></returns>
        private string GetSub(ICollection<Permission> permissions, string userPermissions)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul>");
            foreach (var permission in permissions)
            {
                if (userPermissions.Contains(permission.Code))
                {
                    sb.Append("<li>");
                    sb.Append($"<a href=\"{permission.Url}\">");
                    sb.Append($"{permission.Subject}");
                    sb.Append("</a>");

                    if (permission.Permissions.Any())
                    {
                        sb.Append(GetSub(permission.Permissions, userPermissions));
                    }

                    //if (permission.Permissions.Count() > 0)
                    //{
                    //    sb.Append(GetSub(permission.Permissions, userPermissions));
                    //}
                    sb.Append("</li>");
                }
            }

            sb.Append("</ul>");
            return sb.ToString();

        }
    }
}