using System.Web;
using System.Web.Mvc;
using MVCIdentityDemoApp.Filters;

namespace MVCIdentityDemoApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new CustomAuthzAttribute());
        }
    }
}
