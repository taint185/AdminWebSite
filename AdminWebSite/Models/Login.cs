using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Login
    {

        [Required(ErrorMessage = "Please enter user")]
        [StringLength(200, ErrorMessage = "Username should be atleast 3 charaters", MinimumLength = 3)]
        //Bat loi cho email
        // [EmailAddress(ErrorMessage = "Please enter a vaild user.")]

        public string username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(200, ErrorMessage = "Password should be atleast 3 charaters", MinimumLength = 3)]
        public string password { get; set; }

        public string uri { get; set; }
    }
}