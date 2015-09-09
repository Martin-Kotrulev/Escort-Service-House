using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscortHouseService.Services.Models.BindingModel
{
    public class CustomerEditProfilBindingModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}