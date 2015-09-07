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
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        [ForeignKey("Escort")]
        public string EscortId { get; set; }
        
        public Escort Escort { get; set; }
    }
}
