using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using EscortService.Models.Users;
    using EscortServiceHouse.Data;
    using Models.ViewModels;

    [AllowAnonymous]
    [RoutePrefix("api/Guest")]
    public class GuestController : BaseApiController
    {
        [HttpGet]
        [Route("Escorts")]
        public IHttpActionResult GetAllEscorts()
        {
            var escorts = this.EscortServiceData.Escorts;
            
            List<GuestEscortViewModel> viewModelEscort = new List<GuestEscortViewModel>();

            foreach (var escort in escorts)
            {
                viewModelEscort.Add(new GuestEscortViewModel(escort));
            }

            return this.Ok(viewModelEscort);
        }

        [HttpGet]
        [Route("Escorts/{id}")]
        public IHttpActionResult GetEscortsById(string id)
        {
            Escort escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => e.Id == id);

            if (escort == null)
            {
                return this.NotFound();
            }

           GuestEscortDetailViewModel result = new GuestEscortDetailViewModel(escort);

            return this.Ok(result);
        }
    }


}
