namespace EscortHouseService.Services.Models.ViewModels
{
    using EscortService.Models;

    public class PriceListViewModel
    {
        public PriceListViewModel(PriceList priceList)
        {
            this.EscortName = priceList.Escort.UserName;
            this.ThirtyMinuteRate = priceList.ThirtyMinuteRate;
            this.HourRate = priceList.HourRate;
            this.ThreeHoursRate = priceList.ThreeHoursRate;
            this.SixHoursRate = priceList.SixHoursRate;
            this.OvernightRate = priceList.OvernightRate;
            this.DailyRate = priceList.DailyRate;
            this.WeekendRate = priceList.WeekendRate;
            this.WeeklyRate = priceList.WeeklyRate;
        }

        public string EscortName { get; set; }

        public decimal? ThirtyMinuteRate { get; set; }

        public decimal? HourRate { get; set; }

        public decimal? ThreeHoursRate { get; set; }

        public decimal? SixHoursRate { get; set; }

        public decimal? OvernightRate { get; set; }

        public decimal? DailyRate { get; set; }

        public decimal? WeekendRate { get; set; }

        public decimal? WeeklyRate { get; set; }  
    }
}