using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscortService.Models.Users
{
    public class Admin : ApplicationUser
    {
        public bool IsDeleted { get; set; }
    }
}
