using System.Web;
using System.Web.Mvc;

namespace U2_W5_D4_BACK
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
