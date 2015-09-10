namespace EscortHouseService.Services.Models.ViewModels
{
    using EscortService.Models;

    public class PictureViewModel
    {
        public PictureViewModel()
        {            
        }

        public PictureViewModel(Picture picture)
        {
            this.Id = picture.Id;
            this.B64 = picture.B64;
        }

        public int Id { get; set; }

        public string B64 { get; set; }
    }
}