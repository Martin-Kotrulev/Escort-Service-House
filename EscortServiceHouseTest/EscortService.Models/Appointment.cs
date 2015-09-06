using EscortService.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscortService.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public bool? IsApproved { get; set; }

        [Required]
        public bool IsCanceled { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        public string EscortId { get; set; }

        [ForeignKey("EscortId")]
        public virtual Escort Escort { get; set; }
    }
}