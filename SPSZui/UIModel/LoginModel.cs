using System;
using System.ComponentModel.DataAnnotations;

namespace SPSZui.UIModel
{
    public class LoginModel
    {

        [Required (ErrorMessage = "Login ID je povinný údaj")]
        public int LoginID { get; set; }
        [Required (ErrorMessage = "Password je povinný údaj")]
        public string Password { get; set; }

    }
}
