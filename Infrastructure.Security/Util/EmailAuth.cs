using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Util
{
    class EmailAuth : IEmail
    {
        public string UserName { get ; set ; }
        public string Password { get ; set ; }
        public string Adress { get ; set ; }
        public string DisplayName { get ; set ; }
        public string ServerSmpt { get ; set ; }
        public int Port { get ; set ; }
        public string Credentials { get ; set ; }
        public bool EnableSsl { get ; set ; }
    }
}
