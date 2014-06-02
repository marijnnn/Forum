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
        public string Password
        {
            get;
            set;
        }
    }
}