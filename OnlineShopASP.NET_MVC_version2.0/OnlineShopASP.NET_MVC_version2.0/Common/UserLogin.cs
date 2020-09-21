using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopASP.NET_MVC_version2._0.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string Username { get; set; }
    }
}