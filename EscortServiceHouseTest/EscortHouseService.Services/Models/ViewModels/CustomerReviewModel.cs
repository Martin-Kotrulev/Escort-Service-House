namespace EscortHouseService.Services.Models.ViewModels
{
    using System;
    using EscortService.Models;

    public class CustomerReviewModel
    {
        public CustomerReviewModel(Review review)
        {
            this.Content = review.Content;
            this.Date = review.Date;
            this.Rating = review.Rating;
            this.EscortName = review.Escort.UserName;
        }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }

        public string EscortName { get; set; }

    }
}