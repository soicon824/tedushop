using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Input full name")]
        public string FullName { set; get; }

        [Required(ErrorMessage ="Input user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage ="Input passwrod")]
        [MinLength(6, ErrorMessage ="6 ky tu")]
        public string Password { set; get; }

        [Required(ErrorMessage ="Input email")]
        [EmailAddress(ErrorMessage ="sai email")]
        public string Email { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
    }
}