using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Data.Entity;
    using System.Web.Security;
    using EscortService.Models.Users;
    using Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/admin")]
    public class AdminController : BaseApiController
    {
        [HttpGet]
        [Route("Escorts")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAllEscorts()
        {
            var escorts = this.EscortServiceData.Escorts;
            List<GuestEscortDetailViewModel> viewModelEscort = new List<GuestEscortDetailViewModel>();

            foreach (var escort in escorts)
            {
                viewModelEscort.Add(new GuestEscortDetailViewModel(escort));
            }

            return this.Ok(viewModelEscort);
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAllUsers()
        {
            var users = this.EscortServiceData.Users;
            List<UserViewModel> viewModelUser = new List<UserViewModel>();

            foreach (var user in users)
            {
                viewModelUser.Add(new UserViewModel(user));
            }

            return this.Ok(viewModelUser);
        }


        [HttpGet]
        [Route("role/change")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ChangeUserRole(string userName, string role)
        {
            var user = this.EscortServiceData.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return this.NotFound();
            }

            if (!Roles.RoleExists(role))
            {
                return this.BadRequest(String.Format("Role {0} does not exist!", role));
            }

            if (!Roles.IsUserInRole(user.Id, "Admin"))
            {
                return this.Unauthorized();
            }

            //Roles.RemoveUserFromRole("TestOne", "ExampleRole1");

            Roles.AddUserToRole(user.Id, "Admin");
          
            return this.Ok(String.Format("User {0} added to admin role", user.UserName));
        }
    }
}
