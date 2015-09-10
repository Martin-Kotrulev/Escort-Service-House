using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.ViewModels;
    using System.Web.Http.OData;

    [AllowAnonymous]
    [RoutePrefix("api/Guest")]
    public class GuestController : BaseApiController
    {
        //GET: api/guest/escorts
        [HttpGet]
        [Route("Escorts")]
        [EnableQuery]
        public IHttpActionResult GetAllEscorts()
        {
            var escorts = this.EscortServiceData.Escorts.Where(e => !e.IsDeleted);          
            List<GuestEscortViewModel> viewModelEscort = new List<GuestEscortViewModel>();

            foreach (var escort in escorts)
            {
                viewModelEscort.Add(new GuestEscortViewModel(escort));
            }

            return this.Ok(viewModelEscort);
        }

        //GET: api/escorts/count
        [HttpGet]
        [Route("Escorts/count")]
        public IHttpActionResult GetEscortsCount()
        {
            var result = this.EscortServiceData.Escorts.Count(e => e.IsDeleted);

            return this.Ok(result);
        }

        //GET: api/escorts/{name}
        [HttpGet]
        [Route("Escorts/{name}")]
        public IHttpActionResult GetEscortInfo(string name)
        {
            var escort = this.EscortServiceData.Escorts.FirstOrDefault(e => !e.IsDeleted  && e.UserName == name);

            if (escort == null) return this.BadRequest();

            var result = new GuestEscortDetailViewModel(escort);

            return this.Ok(result);
        }
    }
}
