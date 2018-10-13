using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.WebSocketServer
{
    public class WebSocketServerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WebSocketServer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WebSocketServer_default",
                "WebSocketServer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
