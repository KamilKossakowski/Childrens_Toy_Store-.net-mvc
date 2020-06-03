using System.Web;
using System.Web.Mvc;

namespace Childrens_Toy_Store
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
