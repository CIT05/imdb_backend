using WebApi.Models.Ratings;
using WebApi.Models.TitlePrincipals;
using WebApi.Models.TitleALternatives;
using WebApi.Models.TitleEpisodes;
using WebApi.Models.Genres;
using DataLayer.Ratings;
using DataLayer.TitlePrincipals;
using DataLayer.TitleAlternatives;
using WebApi.Models.TitlePrincipals;
using WebApi.Models.KnownFors;
using WebApi.Models.Production;
using System.Text.Json.Serialization;


namespace WebApi.Models.Titles
{
    public class TitleModel
    {
        public string? Url { get; set; }

        public string PrimaryTitle { get; set; } = string.Empty;

        public string OriginalTitle { get; set; } = string.Empty;

        public bool IsAdult { get; set; }

        public string? StartYear { get; set; } = string.Empty;

        public string? EndYear { get; set; }

        public int? RuntimeMinutes { get; set; }

        public string? Plot { get; set; }

        public string? Poster { get; set; }

        public RatingModel? Rating { get; set; }

        public List<TitleAlternativeModel> TitleAlternatives { get; set; }

        public List<GenreModel> Genres { get; set; }
        public List<TitlePrincipalDTO> Principals { get; set; }

        [JsonIgnore]
        public List<KnownForModel> KnownFors { get; set; }

        public List<ProductionModel> ProductionPersons { get; set; }

        public List<TitleEpisodeModel> Episodes { get; set;}
    }
}
