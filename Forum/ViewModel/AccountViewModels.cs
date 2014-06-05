using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forum
{
    public class LoginModel
    {
        [DisplayName("Gebruikersnaam")]
        [Required(ErrorMessage = "Een gebruikersnaam is verplicht.")]
        [StringLength(20, ErrorMessage = "De gebruikersnaam mag maximaal 20 tekens bevatten.")]
        public string Username
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Een wachtwoord is verplicht.")]
        [DisplayName("Wachtwoord")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
    }

    public class RegisterModel : LoginModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }

    public class MenuModel
    {
        public Account Account
        {
            get
            {
                return Current.Account;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return Current.IsLoggedIn;
            }
        }
    }
}