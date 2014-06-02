using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forum
{
    public class MessageViewModel
    {
        [DisplayName("Bericht")]
        [Required(ErrorMessage = "Een bericht is verplicht.")]
        public string Text
        {
            get;
            set;
        }
    }
}