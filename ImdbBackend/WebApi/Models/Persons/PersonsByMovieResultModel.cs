
namespace WebApi.Models.Persons
{
    public class PersonsByMovieResultModel
    {
        public string? Url { get; set; } = string.Empty;

        public PersonModel? Person { get; set; }

        public double? PersonRating { get; set; }

        public string? PersonRatingUrl { get; set; } = string.Empty;
    }
}

