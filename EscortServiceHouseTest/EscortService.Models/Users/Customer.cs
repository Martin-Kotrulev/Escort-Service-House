using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscortService.Models.Users
{
    public class Customer : ApplicationUser
    {
        private ICollection<Review> reviewsPosted;
        private ICollection<Appointment> appointments;

        public Customer()
        {
            this.reviewsPosted = new HashSet<Review>();
            this.appointments = new HashSet<Appointment>();
        }

        public bool IsDeleted { get; set; }

        public ICollection<Review> ReviewsPosted
        {
            get
            {
                return this.reviewsPosted;
            }

            set
            {
                this.reviewsPosted = value;
            }
        }

        public ICollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }

            set
            {
                this.appointments = value;
            }
        }
    }
}