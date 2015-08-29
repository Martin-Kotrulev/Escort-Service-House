using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using EscortServiceHouse.Data;
    using Microsoft.AspNet.Identity;

    public class BaseApiController : ApiController
    {
        private EscortServiceHouseEntities escortServiceData;

        public BaseApiController()
            : this(new EscortServiceHouseEntities())
        {
        }

        public BaseApiController(EscortServiceHouseEntities escortServiceData)
        {
            this.EscortServiceData = escortServiceData;
        }

        protected EscortServiceHouseEntities EscortServiceData
        {
            get
            {
                return this.escortServiceData;
            }
            private set
            {
                this.escortServiceData = value;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }
    }
}
