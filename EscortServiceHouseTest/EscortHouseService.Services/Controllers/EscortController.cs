namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Http;
    using EscortService.Models;
    using Microsoft.AspNet.Identity;
    using Models.BindingModel;
    using Models.ViewModels;
    using System;
    using System.Data.Entity.Core.Common.CommandTrees;
    using System.Net;
    using System.Web.Http.ModelBinding;

    [Authorize]
    [RoutePrefix("api/escort")]
    public class EscortController : BaseApiController
    {
        private const int PAGE_SIZE = 10;
        private const int PAGE = 1;
       
        // POST api/escort/appointments
        [HttpPost]
        [Route("appointments")]
        public IHttpActionResult GetEscortAppointments([FromBody] AppointmentsBindingModel appModel)
        {          
            if (appModel == null)
            {
                return this.BadRequest("Missing appointments binding model data");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            int pageSize = appModel.PageSize ?? PAGE_SIZE;
            int page = appModel.Page ?? PAGE;

            if (page == 0)
            {
                page = PAGE;
            }

            if (pageSize == 0)
            {
                pageSize = PAGE_SIZE;
            }
            var skip = pageSize * (page - 1);
            string id = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(u => u.IsDeleted == false && u.Id == id);

            if (escort == null)
            {
                return this.NotFound();
            }

            var appointments = this.EscortServiceData.Appointments
                .Where(a => a.EscortId == id && !a.IsCanceled)
                .OrderByDescending(a => a.StartTime).ToList();

            //search logic
            if (appModel.SearchByDate != null)
            {
                DateTime searchDate;

                if (DateTime.TryParse(appModel.SearchByDate, out searchDate))
                {
                    appointments = appointments.Where(a => a.StartTime <= searchDate && a.EndTime >= searchDate).ToList();
                }

            }

            if (appModel.SearchByCustomer != null)
            {
                appointments =
                    appointments.Where(a => a.Customer.UserName.ToLower() == appModel.SearchByCustomer.ToLower())
                        .ToList();
            }

            if (appModel.Location != null)
            {
                appointments = appointments.Where(a => a.Location.ToLower() == appModel.Location.ToLower()).ToList();
            }//end search logic


            //filter logic (Date is not expired)
            //"expired" for time
            //"notExpired" for time
            //"isApproved"
            //"isNotApproved"
            //"isCanceled"
            //"isNotCanceled"
            //"Valid"
            if (appModel.Filter != null)
            {
                var filter = appModel.Filter.ToLower();

                switch (filter)
                {
                    case "expired" :
                        appointments = appointments.Where(a => a.StartTime <= DateTime.Now).ToList();
                        break;
                    case "notexpired" :
                        appointments = appointments.Where(a => a.StartTime > DateTime.Now).ToList();
                        break;
                    case "valid" :
                        appointments = appointments.Where(a => a.StartTime > DateTime.Now && !a.IsCanceled && a.IsApproved == true).ToList();
                        break;
                    case "isapproved" :
                        appointments = appointments.Where(a => a.IsApproved == true).ToList();
                        break;
                    case "isnotapproved":
                        appointments = appointments.Where(a => a.IsApproved == false).ToList();
                        break;
                    case "iscanceled":
                        appointments = appointments.Where(a => a.IsCanceled == true).ToList();
                        break;
                    case "isnotcanceled":
                        appointments = appointments.Where(a => a.IsCanceled == false).ToList();
                        break;
                    default : break;
                }
            }//end filter logic


            //paging using pageSize and Page variables
            if (skip > appointments.Count())
            {
                appointments = appointments.Take(PAGE_SIZE).ToList();
                skip = 0;
            }
            else
            {
                appointments = appointments.Skip(skip).Take(pageSize).ToList();
            }
           
            List<AppointmentViewModel> viewModelAppointments = new List<AppointmentViewModel>();

            foreach (var appointment in appointments)
            {
                viewModelAppointments.Add(new AppointmentViewModel(appointment));
            }

            return this.Ok(new
            {
                Username = escort.UserName,
                Appointments = viewModelAppointments,
                DisplayedAppointments = viewModelAppointments.Count,
                SkippedAppointments = skip,
                AllAppointments = this.EscortServiceData.Appointments.Count(a => a.EscortId == id && !a.IsCanceled)
            });
        }

        // GET api/escort/pricelist
        [Authorize]
        [HttpGet]
        [Route("pricelist")]
        public IHttpActionResult GetPriceList()
        {
            var escortId = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null && escortId != null)
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not escort"
                });
            }

            var priceList = this.EscortServiceData.PriceLists
                .FirstOrDefault(p => p.EscortId == escort.Id);        

            PriceListViewModel result = new PriceListViewModel(priceList);

            return this.Ok(result);
        }

         // GET api/escort/pricelist/edit
        [Authorize]
        [HttpPatch]
        [Route("pricelist/edit")]
        public IHttpActionResult EditPriceList(EditPriceListBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Missing price list data to edit");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var escortId = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null)
            {
                return this.Content(HttpStatusCode.BadRequest,
                    string.Format("There is no existing active escort with Id: {0}", escortId));
            }

            var priceList = this.EscortServiceData.PriceLists
                .FirstOrDefault(p => p.EscortId == escort.Id);

            if (priceList == null)
            {
                return this.NotFound();
            }

            if (model.ThirtyMinuteRate != null)
            {
                priceList.ThirtyMinuteRate = model.ThirtyMinuteRate;
            }

            if (model.HourRate != null)
            {
                priceList.HourRate = model.HourRate;
            }

            if (model.ThreeHoursRate != null)
            {
                priceList.ThreeHoursRate = model.ThreeHoursRate;
            }

            if (model.SixHoursRate != null)
            {
                priceList.SixHoursRate = model.SixHoursRate;
            }

            if (model.OvernightRate != null)
            {
                priceList.OvernightRate = model.OvernightRate;
            }

            if (model.DailyRate != null)
            {
                priceList.DailyRate = model.DailyRate;
            }

            if (model.WeekendRate != null)
            {
                priceList.WeekendRate = model.WeekendRate;
            }

            if (model.WeeklyRate != null)
            {
                priceList.WeeklyRate = model.WeeklyRate;
            }
           
            this.EscortServiceData.PriceLists.AddOrUpdate(priceList);
            this.EscortServiceData.SaveChanges();

            return this.Ok(new
            {
                Message = "Escort with id: " + escortId + "  patched."
            });
        }

        //PATCH:  api/escort/appointments/{id}/cancel
        [HttpPatch]
        [Route("appointments/{id}/cancel")]
        public IHttpActionResult CancelAppointment([FromBody]int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var appointment = this.EscortServiceData.Appointments.Find(id);

            if (appointment == null || appointment.EscortId != currentUserId)
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

        //PATCH:  api/escort/appointments/{id}/confirm
        [HttpPatch]
        [Route("appointments/{id}/confirm")]
        public IHttpActionResult ConfirmAppointment([FromUri]int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var appointment = this.EscortServiceData.Appointments.Find(id);

            if (appointment == null || appointment.EscortId != currentUserId)
            {
                return this.BadRequest("Invalid appointment ID");
            }

            if (appointment.IsCanceled == true)
            {
                return this.BadRequest("Appointment already canceled");
            }

            if (appointment.IsApproved != null)
            {
                return this.BadRequest("Appointment state already set");
            }

            if (appointment.EndTime < DateTime.Now)
            {
                return this.BadRequest("Appointment is no longer valid");
            }

            if (this.EscortServiceData.Appointments.Any(a => a.EscortId == currentUserId &&
                ((appointment.StartTime >= a.StartTime && appointment.StartTime <= a.EndTime) ||
                (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime))))
            {
                return this.BadRequest("Escort occupied for that time period");
            }

            appointment.IsApproved = true;
            this.EscortServiceData.SaveChanges();

            return this.Ok();
        }

        //PATCH:  api/escort/appointments/{id}/reject
        [HttpPatch]
        [Route("appointments/{id}/reject")]
        public IHttpActionResult RejectAppointment([FromUri]int id)
        {            
            var currentUserId = this.User.Identity.GetUserId();
            var appointment = this.EscortServiceData.Appointments.Find(id);

            if (appointment == null || appointment.EscortId != currentUserId)
            {
                return this.BadRequest("Invalid appointment ID");
            }

            if (appointment.IsCanceled == true)
            {
                return this.BadRequest("Appointment already canceled");
            }

            if (appointment.IsApproved != null)
            {
                return this.BadRequest("Appointment state already set");
            }

            if (appointment.EndTime < DateTime.Now)
            {
                return this.BadRequest("Appointment is no longer valid");
            }

            if (this.EscortServiceData.Appointments.Any(a => a.EscortId == currentUserId &&
                ((appointment.StartTime >= a.StartTime && appointment.StartTime <= a.EndTime) ||
                (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime))))
            {
                return this.BadRequest("Escort occupied for that time period");
            }

            appointment.IsApproved = false;
            this.EscortServiceData.SaveChanges();

            return this.Ok();
        }

        //POST:  api/escort/pictures/add
        [HttpPost]
        [Route("pictures/add")]
        public IHttpActionResult EscortAddPicture([FromBody] PictureBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Missing picture data");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var escortId = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null && escortId != null)
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not escort"
                });
            }

            if (this.EscortServiceData.Pictures.Count(p => p.EscortId == escort.Id) > 10)
            {
                return this.BadRequest("An escort allready has 10 pictures.");
            }

            if (this.EscortServiceData.Pictures.Any(p => p.B64 == model.B64))
            {
                return this.Content(HttpStatusCode.Conflict,
                    "This picture already exist");
            }

            var newPicture = new Picture()
            {
                EscortId = escort.Id,
                B64 = model.B64,
                IsProfile = false
            };

            this.EscortServiceData.Pictures.Add(newPicture);
            this.EscortServiceData.SaveChanges();

            return this.Content(HttpStatusCode.OK,
                string.Format("Picture with id: {0}   added successfully to escort: {1} pictures gallery.",
                    newPicture.Id, escort.UserName));
        }

        //DELETE:  api/escort/pictures/{id}/delete
        [HttpDelete]
        [Route("pictures/{id:int}/delete")]
        public IHttpActionResult DeletePictureById(int id)
        {
            var escortId = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null && escortId != null)
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not escort"
                });
            }

            Picture picture = this.EscortServiceData.Pictures.FirstOrDefault(p => p.Id == id);

            if (picture == null)
            {
                return this.Content(HttpStatusCode.OK, new
                {
                    Message = string.Format("There is no picture with id: {0}", id)
                });              
            }

            if (picture.EscortId != escort.Id )
            {
                return this.Unauthorized();
            }

            if (picture.IsProfile)
            {
                return this.BadRequest(string.Format("Picture with id: {0} is profile picture. First you have to select new profile picture", id));
            }

            this.EscortServiceData.Pictures.Remove(picture);
            this.EscortServiceData.SaveChanges();

            return this.Content(HttpStatusCode.OK, new
            {
                Message = "Picture wit id: " + picture.Id + " deleted successfully."
            });
        }

        //PUT:  api/escort/pictures/{id}/change
        [HttpPut]
        [Route("pictures/{id:int}/change")]
        public IHttpActionResult ChangeProfilePicture(int id)
        {
            var escortId = this.User.Identity.GetUserId();
            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null && escortId != null)
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not escort"
                });
            }

            Picture newPicture = this.EscortServiceData.Pictures.FirstOrDefault(p => p.Id == id);

            if (newPicture == null)
            {

                return this.Content(HttpStatusCode.OK, new
                {
                    Message = string.Format("There is no picture with id: {0}", id)
                });
            }

            Picture oldPicture = this.EscortServiceData.Pictures.FirstOrDefault(p => p.EscortId == escort.Id && p.IsProfile);

            if (newPicture.EscortId != escort.Id)
            {
                return this.Unauthorized();
            }

            oldPicture.IsProfile = false;
            newPicture.IsProfile = true;
            this.EscortServiceData.SaveChanges();

            return this.Content(HttpStatusCode.OK, new
            {
                Message =
                    string.Format("Picture with id: {0} was made profile one for escort: {1}", newPicture.Id,
                        escort.UserName)
            });
        }

        //DELETE:  api/escort/delete
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteLogedEscort()
        {
            var escortId = this.User.Identity.GetUserId();

            if (escortId == null)
            {
                return this.Unauthorized();
            }

            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => !e.IsDeleted && e.Id == escortId);

            if (escort == null && escortId != null)
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not escort"
                });
            }


            escort.IsDeleted = true;
            this.EscortServiceData.SaveChanges();

            return this.Content(HttpStatusCode.OK, new
            {
                Message = string.Format("Escort: {0} deleted successfully", escort.UserName)
            });
        }
    }
}
