namespace WebApi.Models.Ratings
{
    public class RatingModel
    {
        public string? Url { get; set; }

        public decimal AverageRating { get; set; }

        public int NumberOfVotes { get; set; }

    }
}
