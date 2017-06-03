using System.Web.Mvc;

namespace eTender.Areas.Committee
{
    public class CommitteeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Committee";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Committee_default",
                "Committee/{controller}/{action}/{id}",
                new { controller = "CDash", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
