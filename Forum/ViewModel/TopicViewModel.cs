using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forum
{
    public class TopicModel
    {
        [DisplayName("Titel")]
        [Required(ErrorMessage = "Een titel is verplicht.")]
        [StringLength(20, ErrorMessage = "De titel mag maximaal 20 tekens bevatten.")]
        public string Title
        {
            get;
            set;
        }

        [DisplayName("Bericht")]
        [Required(ErrorMessage = "Een bericht is verplicht.")]
        public string Text
        {
            get;
            set;
        }
    }
}