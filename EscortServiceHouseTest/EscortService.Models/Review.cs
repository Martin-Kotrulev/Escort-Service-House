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
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
                
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Customer Author { get; set; }
                
        public string EscortId { get; set; }

        [ForeignKey("EscortId")]
        public virtual Escort Escort { get; set; }
    }
}
