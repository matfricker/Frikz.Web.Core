using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Frikz.Web.Core
{
    public static class RegistrationSession
    {
        private const string sessionName = "RegistrationSession";

        public static DataSet dsRegistrationSession
        {
            get
            {
                return IsRegistrationSessionNull()
                    ? null : System.Web.HttpContext.Current.Session[sessionName] as DataSet;
            }
        }

        public static bool IsRegistrationSessionNull()
        {
            if (System.Web.HttpContext.Current.Session[sessionName] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddRegistrationSession(DataSet ds)
        {
            System.Web.HttpContext.Current.Session.Add(sessionName, ds);
        }
        
        public static void ClearRegistrationSession()
        {
            System.Web.HttpContext.Current.Session.Clear();
        }

        
    }
}
