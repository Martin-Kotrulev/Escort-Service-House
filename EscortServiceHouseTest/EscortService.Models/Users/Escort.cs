using EscortService.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscortService.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class Escort : ApplicationUser
    {
        //private const string RoleName = "Escort";

        private ICollection<Service> services;
        private ICollection<Picture> pictures;
        private ICollection<Review> reviewsReceived;
        private ICollection<Appointment> appointments;

        public Escort()
        {
            this.services = new HashSet<Service>();
            this.pictures = new HashSet<Picture>();
            this.reviewsReceived = new HashSet<Review>();
            this.appointments = new HashSet<Appointment>();
        }        

        public string Description { get; set; }

        public string HairColour { get; set; }

        public string BreastsSize { get; set; }

        public BreastsType BrastsType { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }

        [Required]
        public string Town { get; set; }
        
        public virtual PriceList Prices { get; set; }

        public virtual ICollection<Service> Services
        { 
            get
            {
                return this.services;
            }

            set
            {
                this.services = value;
            }
        }

        public virtual ICollection<Picture> Pictures
        {
            get
            {
                return this.pictures;
            }

            set
            {
                this.pictures = value;
            }
        }

        public virtual ICollection<Review> ReviewsReceived
        {
            get
            {
                return this.reviewsReceived;
            }

            set
            {
                this.reviewsReceived = value;
            }
        }

        public virtual ICollection<Appointment> Appointments
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