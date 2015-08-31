namespace EscortHouseService.Services.Models.BindingModel
{
    public class EditPriceListBindingModel
    {
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