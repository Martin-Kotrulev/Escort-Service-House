using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using EscortService.Models.Users;
    using EscortServiceHouse.Data;
    using Models.ViewModels;
    using Microsoft.Web;
    using System.Web.Http.OData;

    [AllowAnonymous]
    [RoutePrefix("api/Guest")]
    public class GuestController : BaseApiController
    {
        [HttpGet]
        [Route("Escorts")]
        [EnableQuery]
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
        [Route("Escorts/count")]
        public IHttpActionResult GetEscortsCount()
        {
            var result = this.EscortServiceData.Escorts.Count();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("Escorts/{name}")]
        public IHttpActionResult GetEscortInfo(string name)
        {
            var escort = this.EscortServiceData.Escorts.FirstOrDefault(e => e.UserName == name);

            if (escort == null) return this.BadRequest();

            var result = new GuestEscortDetailViewModel(escort);

            return this.Ok(result);
        }
    }
}
