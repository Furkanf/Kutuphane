using Kutuphane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kutuphane.Session
{
    public static class CurrentSession
    {
        public static AdminUser getCurrentAdmin()
        {
            if (HttpContext.Current.Session["admin"] == null) return null;
            else 
            { 
                AdminUser currentAdmin = (AdminUser)HttpContext.Current.Session["admin"];
                return currentAdmin;
            }
            
        }

        public static StandardUser getCurrentUser()
        {
            if (HttpContext.Current.Session["user"] == null) return null;
            else
            {
                StandardUser currentUser = (StandardUser)HttpContext.Current.Session["user"];
                return currentUser;
            }

        }
    }
}