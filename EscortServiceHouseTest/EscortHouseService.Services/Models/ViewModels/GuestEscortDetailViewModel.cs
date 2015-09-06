namespace EscortHouseService.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EscortService.Models;
    using EscortService.Models.Enumerations;
    using EscortService.Models.Users;
    using System.Linq;

    public class GuestEscortDetailViewModel : GuestEscortViewModel
    {
        //private ICollection<PictureViewModel> pictures;

        public GuestEscortDetailViewModel(Escort escort) : base(escort)
        {
            this.Description = escort.Description == null ? "--" : escort.Description;
            this.Town = escort.Town;           
            this.Pictures = escort.Pictures.Select(p => p.B64).ToArray();
            var prices = escort.Prices;
            if (prices == null) this.HourRate = "--";
            else this.HourRate = prices.HourRate.ToString();
        }

        public string Description { get; set; }

        public string Town { get; set; }

        public string HourRate { get; set; }

        public string[] Pictures { get; set; }
    }
}