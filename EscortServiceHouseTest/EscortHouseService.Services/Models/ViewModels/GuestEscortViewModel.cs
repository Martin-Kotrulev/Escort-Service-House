using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscortHouseService.Services.Models.ViewModels
{
    using EscortService.Models;
    using EscortService.Models.Enumerations;
    using EscortService.Models.Users;

    public class GuestEscortViewModel
    {
        public GuestEscortViewModel(Escort escort)
        {
            this.UserName = escort.UserName;
            this.Gender = escort.Gender.ToString();
            this.B64Profile = escort.Pictures.FirstOrDefault(p => p.IsProfile == true).B64;
        }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public string B64Profile { get; set; }
    }
}