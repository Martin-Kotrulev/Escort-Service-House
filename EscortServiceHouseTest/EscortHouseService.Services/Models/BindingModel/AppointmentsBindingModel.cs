namespace EscortHouseService.Services.Models.BindingModel
{
    public class AppointmentsBindingModel
    {
        public int? PageSize { get; set; }

        public int? Page { get; set; }

        public string SearchByDate { get; set; }

        public string SearchByCustomer { get; set; }

        public string Location { get; set; }

        public string Filter { get; set; }
    }
}