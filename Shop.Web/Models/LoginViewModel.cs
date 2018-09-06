using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Phai nhap Name")]
        public string UserName { set; get; }
        [Required(ErrorMessage ="Nhap pass")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        public bool RememberMe { get; set; }
    }
}