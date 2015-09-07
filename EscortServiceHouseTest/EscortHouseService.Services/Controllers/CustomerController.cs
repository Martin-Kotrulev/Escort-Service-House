using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Threading;
    using EscortService.Models;
    using EscortService.Models.Users;
    using Microsoft.AspNet.Identity;
    using Models.BindingModel;
    using Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseApiController
    {
        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetLogedCustomer()
        {
            string customerId = this.User.Identity.GetUserId();

            Customer customer = this.EscortServiceData.Customers
                .FirstOrDefault(c => !c.IsDeleted && c.Id == customerId);

            if (customer == null)
            {
                return this.Content(HttpStatusCode.NotFound,
                    string.Format("There is no such active customer with Id: {0}!", customerId));
            }

            var appointmentsMade = this.EscortServiceData.Appointments
                .Where(a => a.CustomerId == customerId).ToList();
            var reviewsPosted = this.EscortServiceData.Reviews
                .Where(a => a.AuthorId == customerId).ToList();
            DetailedCustomerViewModel result = new DetailedCustomerViewModel(customer, appointmentsMade, reviewsPosted);

            return this.Ok(result);
        }

        [HttpPost]
        [Route("appointment/submit")]
        public IHttpActionResult SubmitAppointment(SubmitAppointmentBindingModel appData)
        {
            if (appData == null)
            {
                return this.BadRequest("Missing appointment data");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = this.User.Identity.GetUserId();

            if (!this.EscortServiceData.Customers.Any(c => c.Id == currentUserId))
            {
                return this.Unauthorized();
            }

            var escortId =
                this.EscortServiceData.Escorts.Where(c => !c.IsDeleted && c.UserName == appData.EscortName).Select(c => c.Id).FirstOrDefault();

            if (escortId == null)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    string.Format("There is no existing active escort with user name: {0}", appData.EscortName));
            }

            //We don't need to list all escort's appointments here. We can just check whether there is an appointment in this time period.
            var appointmentsForEscort = this.EscortServiceData.Appointments.Where(a => a.EscortId == escortId).ToList();
            var appStartTime = DateTime.Parse(appData.StartTime);
            var appEndTime = DateTime.Parse(appData.EndTime);

            foreach (var appointment in appointmentsForEscort)
            {
                if ((appStartTime >= appointment.StartTime && appStartTime <= appointment.EndTime) || (appEndTime >= appointment.StartTime && appEndTime <= appointment.EndTime))
                {
                    return
                        this.BadRequest(String.Format("Escort {0} is occupiet in period: {1}  to  {2}",
                            appData.EscortName, appointment.StartTime, appointment.EndTime));
                }
            }

            Appointment newAppointment = new Appointment()
            {
                StartTime = DateTime.Parse(appData.StartTime),
                EndTime = DateTime.Parse(appData.EndTime),
                Price = appData.Price,
                Location = appData.Location,
                IsApproved = false,
                IsCanceled = false,
                CustomerId = currentUserId,
                EscortId = escortId,
            };

            this.EscortServiceData.Appointments.Add(newAppointment);
            this.EscortServiceData.SaveChanges();

            return
                this.Ok(String.Format(
                    "Appointment from customer with id: {0} to escort with username: {1} was submited", currentUserId,
                    appData.EscortName));
        }

        [HttpPatch]
        [Route("appointment/{id}/cancel")]
        public IHttpActionResult CancelAppointment([FromUri]int id)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var appointment = this.EscortServiceData.Appointments.Find(id);

            if (appointment == null || appointment.CustomerId != currentUserId)
            {
                return this.BadRequest("Invalid appointment ID");
            }

            if (appointment.IsCanceled == true)
            {
                return this.BadRequest("Appointment already canceled");
            }

            if (appointment.EndTime < DateTime.Now)
            {
                return this.BadRequest("Appointment is no longer valid");
            }

            appointment.IsCanceled = true;

            this.EscortServiceData.SaveChanges();

            return this.Ok();
        }
    }
}
