using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscortHouseService.Services.Models.BindingModel
{
    using System.ComponentModel.DataAnnotations;
    using EscortService.Models;

    public class ReviewBindingModel
    {
        [Required]
        public string Content { get; set; }

        [Range(0,5)]
        public int Rating { get; set; }

        [Required]
        public string EscortName { get; set; }
    }
}