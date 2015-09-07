namespace EscortHouseService.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Controllers;
    using EscortService.Models.Users;

    public class UserViewModel : BaseApiController
    {
        private List<string> roles; 

        public UserViewModel()
        {
            
        }

        public UserViewModel(ApplicationUser user)
        {
            this.UserId = user.Id;
            this.UserName = user.UserName;  
            this.roles = new List<string>();

            foreach (var role in user.Roles)
            {
                roles.Add(this.EscortServiceData.Roles.Where(r => r.Id == role.RoleId).Select(r => r.Name).ToString());
            }
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }
    }
}