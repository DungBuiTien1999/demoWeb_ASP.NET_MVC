using System.Web;
using System.Web.Mvc;

namespace OnlineShopASP.NET_MVC_version2._0
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
