namespace EscortHouseService.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EscortService.Models;
    using EscortService.Models.Enumerations;
    using EscortService.Models.Users;

    public class GuestEscortDetailViewModel : GuestEscortViewModel
    {
        //private ICollection<PictureViewModel> pictures;

        public GuestEscortDetailViewModel(Escort escort) : base(escort)
        {
            this.Description = escort.Description;
            this.HairColour = escort.HairColour;
            this.BreastsSize = escort.BreastsSize;
            this.BreastsType = escort.BreastsType.ToString();
            this.Height = escort.Height;
            this.Weight = escort.Weight;
            this.Town = escort.Town;
            //this.pictures = new List<PictureViewModel>();

            //foreach (var picture in escort.Pictures)
            //{
            //    this.pictures.Add(new PictureViewModel(picture));
            //}
        }

        public string Description { get; set; }

        public string HairColour { get; set; }

        public string BreastsSize { get; set; }

        public string BreastsType { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }

        public string Town { get; set; }

        //public virtual ICollection<PictureViewModel> Pictures
        //{
        //    get { return this.pictures; }
        //    set { this.pictures = value; }
        //}
    }
}