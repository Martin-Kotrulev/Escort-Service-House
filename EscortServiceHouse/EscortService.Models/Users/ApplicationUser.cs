using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using EscortService.Models.Enumerations;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace EscortService.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public override string PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int Age { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager,
        string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
