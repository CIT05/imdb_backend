using WebApi.Models.Ratings;


namespace WebApi.Models.Titles
{
    public class TitleModel
    {
        public string? Url { get; set; }

        public string PrimaryTitle { get; set; } = string.Empty;

        public string OriginalTitle { get; set; } = string.Empty;

        public bool IsAdult { get; set; }

        public string? startYear { get; set; } = string.Empty;

        public string? endYear { get; set; }

        public int? RuntimeMinutes { get; set; }

        public string? Plot { get; set; }

        public string? Poster { get; set; }

        public RatingModel? Rating { get; set; }

        //public List<TitlePrincipal> Principals { get; set; }

        //public List<TitleAlternative> TitleAlternatives { get; set; }
    }
}
