namespace EscortHouseService.Services.Models.ViewModels
{
    using System;
    using EscortService.Models;

    public class CustomerAppointmentViewModel
    {      

        public CustomerAppointmentViewModel(Appointment appointment)
        {
            this.Id = appointment.Id;
            this.StartTime = appointment.StartTime;
            this.EndTime = appointment.EndTime;
            this.Price = appointment.Price;
            this.Location = appointment.Location;
            this.EscortName = appointment.Escort.UserName;
            this.IsCanceled = appointment.IsCanceled;
            if (appointment.IsApproved != null)
            {
                this.IsApproved = (bool) appointment.IsApproved;
            }
            this.IsExpired = DateTime.Now > appointment.EndTime;
        }

        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string EscortName { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsApproved { get; set; }

        public bool IsExpired { get; set; }
    }
}