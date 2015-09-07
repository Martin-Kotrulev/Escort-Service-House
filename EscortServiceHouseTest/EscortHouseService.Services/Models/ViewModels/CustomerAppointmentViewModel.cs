namespace EscortHouseService.Services.Models.ViewModels
{
    using System;
    using EscortService.Models;

    public class CustomerAppointmentViewModel
    {
        public CustomerAppointmentViewModel(Appointment appointment)
        {
            this.StartTime = appointment.StartTime;
            this.EndTime = appointment.EndTime;
            this.Price = appointment.Price;
            this.Location = appointment.Location;
            this.EscortName = appointment.Escort.UserName;
        }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string EscortName { get; set; } 
    }
}