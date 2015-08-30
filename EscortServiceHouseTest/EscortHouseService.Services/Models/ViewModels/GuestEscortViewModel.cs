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
        private ICollection<PictureViewModel> pictures;

        public GuestEscortViewModel(Escort escort)
        {
            this.UserName = escort.UserName;
            this.Gender = escort.Gender.ToString();
            this.pictures = new List<PictureViewModel>();

            foreach (var picture in escort.Pictures)
            {
                this.pictures.Add(new PictureViewModel(picture));
            }
        }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<PictureViewModel> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}