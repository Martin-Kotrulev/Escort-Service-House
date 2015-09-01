namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Http;
    using System.Web.UI;
    using EscortService.Models;
    using Microsoft.AspNet.Identity;
    using Models.BindingModel;
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

        // GET api/escort/pricelist
        [Authorize]
        [HttpGet]
        [Route("pricelist")]
        public IHttpActionResult GetPriceList()
        {
            var escortId = this.User.Identity.GetUserId();

            var escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => e.Id == escortId);

            if (escort == null)
            {
                return this.Unauthorized();
            }

            var priceList = this.EscortServiceData.PriceLists
                .FirstOrDefault(p => p.EscortId == escortId);


            if (priceList == null)
            {
                var newPriceList = new PriceList
                {
                    EscortId = escortId,
                    ThirtyMinuteRate = null,
                    HourRate = null,
                    ThreeHoursRate = null,
                    SixHoursRate = null,
                    OvernightRate = null,
                    DailyRate = null,
                    WeekendRate = null,
                    WeeklyRate = null
                };

                this.EscortServiceData.PriceLists.Add(newPriceList);
                this.EscortServiceData.SaveChanges();

                priceList = this.EscortServiceData.PriceLists
                .FirstOrDefault(p => p.EscortId == escortId);
            }

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
            var priceList = this.EscortServiceData.PriceLists
                .FirstOrDefault(p => p.EscortId == escortId);

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
    }
}
