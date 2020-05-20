using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutuphane.Filters
{
    public class AdminAuth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["admin"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Security/Login");
            }
        }
    }
}