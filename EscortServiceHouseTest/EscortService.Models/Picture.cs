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
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string B64 { get; set; }

        [Required]        
        public string EscortId { get; set; }

        [ForeignKey("EscortId")]
        public virtual Escort Owner { get; set; }

        [Required]
        public bool IsProfile { get; set; }
    }
}
