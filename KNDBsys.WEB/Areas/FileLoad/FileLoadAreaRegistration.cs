using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.FileLoad
{
    public class FileLoadAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FileLoad";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FileLoad_default",
                "FileLoad/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
