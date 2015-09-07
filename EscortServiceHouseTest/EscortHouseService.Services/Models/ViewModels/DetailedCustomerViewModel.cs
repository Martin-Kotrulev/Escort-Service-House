namespace EscortHouseService.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using EscortService.Models;
    using EscortService.Models.Users;

    public class DetailedCustomerViewModel
    {
        private ICollection<CustomerReviewModel> reviewsPosted;
        private ICollection<CustomerAppointmentViewModel> appointmentsMade;

        public DetailedCustomerViewModel(Customer customer, List<Appointment> appointments, List<Review> reviews)
        {
            this.Username = customer.UserName;
            this.Email = customer.Email;
            this.PhoneNumber = customer.PhoneNumber;
            this.Gender = customer.Gender.ToString();
            this.reviewsPosted = new List<CustomerReviewModel>();
            this.appointmentsMade = new List<CustomerAppointmentViewModel>();

            foreach (var review in reviews)
            {
                this.reviewsPosted.Add(new CustomerReviewModel(review));
            }

            foreach (var appointment in appointments)
            {
                this.appointmentsMade.Add(new CustomerAppointmentViewModel(appointment));
            }
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<CustomerReviewModel> CustomerReviews
        {
            get { return this.reviewsPosted; }
            set { this.reviewsPosted = value; }
        }

        public virtual ICollection<CustomerAppointmentViewModel> CustomerAppointments
        {
            get { return this.appointmentsMade; }
            set { this.appointmentsMade = value; }
        }
    }
}