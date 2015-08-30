namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Security;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/escort")]
    public class EscortController : BaseApiController
    {
        // GET api/escort/appointments
        [HttpGet]
        [Route("appointments")]
        public IHttpActionResult GetEscortAppointments()
        {
            string id = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts.FirstOrDefault(u => u.Id == id);

            if (escort == null)
            {
                return this.NotFound();
            }

            //if (!Roles.IsUserInRole(this.User.Identity.GetUserName(), "Escort"))       
            //{
            //    return this.Unauthorized();
            //}

            //if (!Roles.IsUserInRole(id, "Escort"))
            //{
            //    return this.Unauthorized();
            //}

            var appointments = this.EscortServiceData.Appointments
                .Where(a => a.EscortId == id && !a.IsCanceled)
                .OrderByDescending(a => a.StartTime);

            List<AppointmentViewModel> viewModelAppointments = new List<AppointmentViewModel>();

            foreach (var appointment in appointments)
            {
                viewModelAppointments.Add(new AppointmentViewModel(appointment));
            }

            return this.Ok(new
            {
                Username = escort.UserName,
                Appointments = viewModelAppointments
            });
        }
    }
}
