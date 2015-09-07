using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscortHouseService.Services.Models.ViewModels
{
    using EscortService.Models;
    using EscortService.Models.Enumerations;
    using EscortService.Models.Users;

    public class GestGetEscortViewModel
    {
        public GestGetEscortViewModel(ApplicationUser escort)
        {

            this.UserName = escort.UserName;

            this.Gender = escort.Gender.ToString();
        }

        public string UserName { get; set; }

        public string Gender { get; set; }
    }
}