using Core.Entities.Contracts;
using Infrastructure.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Security
{
    public class Sistema : ISistema
    {
        public string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public DateTime Now
        {
            get
            {
                return ByADateTime.Now;
            }
        }

        public static DateTime _Now
        {
            get
            {
                return ByADateTime.Now;
            }
        }

        public string IpAddress => GetIP() ?? HttpContext.Current.Request.UserHostAddress;

        public string HostName => HttpContext.Current.Request.ServerVariables["REMOTE_USER"] ?? HttpContext.Current.Request.UserHostName;

        public static string GetIP()
        {
            string ip =
                HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }
    }
}
