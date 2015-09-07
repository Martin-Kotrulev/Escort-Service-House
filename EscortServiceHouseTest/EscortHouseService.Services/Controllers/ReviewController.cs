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
        [Route("{escortName}/add")]
        [HttpPost]
        public IHttpActionResult AddReview(string escortName, ReviewBindingModel reviewModel)
        {
            if (reviewModel == null)
            {
                return this.BadRequest("Missing review data");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = this.User.Identity.GetUserId();

            if (!this.EscortServiceData.Customers.Any(c => c.Id == currentUserId))
            {
                return this.Content(HttpStatusCode.Unauthorized, new
                {
                    Message = "User " + this.User.Identity.GetUserName() + " is not customer"
                });
            }

            var escortId =
                this.EscortServiceData.Escorts.Where(c => c.UserName == reviewModel.EscortName).Select(c => c.Id).FirstOrDefault();

            if (escortId == null)
            {
                return this.Content(HttpStatusCode.NotFound, new
                {
                    Message = "Escort  " + reviewModel.EscortName + " does not exist."
                });
            }

            var lastReview = this.EscortServiceData.Reviews
                .Where(r => r.AuthorId == currentUserId && r.EscortId == escortId)
                .OrderByDescending(r => r.Date)
                .FirstOrDefault();

            if (lastReview != null)
            {
                var dateTiemDiff = (DateTime.Now - lastReview.Date).TotalHours;
                if (dateTiemDiff < 2.0)
                {
                    return this.BadRequest(String.Format("You have given revie for escort: {0} just before {1} hours.",
                        reviewModel.EscortName, dateTiemDiff));
                }   
            }                

            Review newReview = new Review()
            {
                Content = reviewModel.Content,
                Date = DateTime.Now,
                Rating = reviewModel.Rating,
                AuthorId = currentUserId,
                EscortId = escortId,
            };

            this.EscortServiceData.Reviews.Add(newReview);
            this.EscortServiceData.SaveChanges();

            return
                this.Ok(String.Format(
                    "Review for escort: {0} created from customer: {1}", reviewModel.EscortName, this.User.Identity.GetUserName()));      
           
        }
    }
}
