using System.Web.Mvc;

namespace KNDBsys.WEB.Areas.TestView
{
    public class TestViewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TestView";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TestView_default",
                "TestView/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
