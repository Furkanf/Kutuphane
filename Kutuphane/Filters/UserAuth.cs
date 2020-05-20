using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutuphane.Filters
{
    public class UserAuth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(HttpContext.Current.Session["user"]==null)
            {
                filterContext.Result = new RedirectResult("/Security/Login");
            }
        }
    }
}