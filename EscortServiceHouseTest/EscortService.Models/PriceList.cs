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
    public class PriceList
    {
        [Key, ForeignKey("Escort")]
        public string EscortId { get; set; }

        public decimal? ThirtyMinuteRate { get; set; }

        public decimal? HourRate { get; set; }

        public decimal? ThreeHoursRate { get; set; }

        public decimal? SixHoursRate { get; set; }

        public decimal? OvernightRate { get; set; }

        public decimal? DailyRate { get; set; }

        public decimal? WeekendRate { get; set; }

        public decimal? WeeklyRate { get; set; }            
               
        public virtual Escort Escort { get; set; }
    }
}
