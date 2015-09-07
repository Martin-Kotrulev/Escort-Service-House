namespace EscortHouseService.Services.Models.ViewModels
{
    using EscortService.Models;

    public class PictureViewModel
    {
        public PictureViewModel(Picture picture)
        {
            this.B64 = picture.B64;
        }

        public string B64 { get; set; }
    }
}