namespace WebApi.Models.Ratings

{
    public class RatingForUserResultModel
    {
        public string? Url { get; set; }

        public string? TitleUrl { get; set; }

        public string? UserUrl { get; set; }

        public string? TitleOverallRatingUrl { get; set; }

        public DateTime TimeStamp { get; set; }

        public double Rating { get; set; }

    }
}
