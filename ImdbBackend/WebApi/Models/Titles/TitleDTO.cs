



using WebApi.Models.Ratings;

namespace WebApi.Models.Titles
{
    public class TitleDTO
    {
        public string Url { get; set; } = string.Empty;

        public string PrimaryTitle { get; set; } = string.Empty;

        public string? Poster { get; set; }

        public RatingModel? Rating { get; set; }

    }
}
