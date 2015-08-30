namespace EscortHouseService.Services.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EscortService.Models;
    using EscortService.Models.Users;

    public class AppointmentViewModel
    {
        public AppointmentViewModel(Appointment appointment)
        {
            this.StartTime = appointment.StartTime;
            this.EndTime = appointment.EndTime;
            this.Price = appointment.Price;
            this.Location = appointment.Location;
            this.CustomerName = appointment.Customer.UserName;
        }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string CustomerName { get; set; }

    }
}