using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EscortHouseService.Services.Controllers
{
    using EscortService.Models;
    using EscortService.Models.Users;
    using Microsoft.AspNet.Identity;
    using Models.BindingModel;
    using Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/escort-review")]
    public class ReviewController : BaseApiController
    {
       
        //[Authorize(Roles = "Customer")]
        [Route("{escortName}")]
        [HttpPost]
        public IHttpActionResult AddReview(string escortName, ReviewBindingModel escortReview)
        {
            var userId = User.Identity.GetUserId();

            Customer customer = this.EscortServiceData.Customers
                .FirstOrDefault(c => c.Id == userId);

            Escort escort = this.EscortServiceData.Escorts
                .FirstOrDefault(e => e.UserName == escortName);


            if (customer == null)
            {
                return this.Unauthorized();
            }

            if (escort == null)
            {
                return this.BadRequest();
            }

            if (escortReview == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var review = new Review()
            {
                Content = escortReview.Content,
                Date = DateTime.Now,
                Rating = escortReview.Rating,
                AuthorId = customer.Id,
                EscortId = escort.Id
            };
            this.EscortServiceData.Reviews.Add(review);
            this.EscortServiceData.SaveChanges();

            return this.Ok();
        }
    }
}
