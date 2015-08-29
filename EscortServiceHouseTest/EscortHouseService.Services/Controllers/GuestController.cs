using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
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
            
            List<GestGetEscortViewModel> viewModelEscort = new List<GestGetEscortViewModel>();

            foreach (var escort in escorts)
            {
                viewModelEscort.Add(new GestGetEscortViewModel(escort));
            }

            return this.Ok(viewModelEscort);
        }
    }
}
