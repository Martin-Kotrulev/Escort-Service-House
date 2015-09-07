namespace EscortHouseService.Services.Models.BindingModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EscortService.Models.Users;

    public class SubmitAppointmentBindingModel
    {
        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string EscortName { get; set; }

    }
}