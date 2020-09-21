using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Type Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Type Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}