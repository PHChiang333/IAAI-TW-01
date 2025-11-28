using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // 重要：讓 Area 支援 Attribute Routing
            context.Routes.MapMvcAttributeRoutes();



            // 備用傳統路由（可保留）
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}