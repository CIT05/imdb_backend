using WebApi.Models.Persons;
using WebApi.Models.Titles;

namespace WebApi.Models.Searching
{
    public class TitleStringSearchResultModel
    {
        public string Url { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
    }

    public class ActorStringSearchResultModel
    {
        public string Url { get; set; } = string.Empty;

        public string ActorName { get; set; } = string.Empty;
    }


    public class ArtistStringSearchResultModel
    {
        public PersonDTO? Person { get; set; }

        public decimal? Likelihood { get; set; }
    }


    public class TitleExactSearchResultModel
    {
        public TitleModel? Title { get; set; }
    }

    public class TitleBestSearchResultModel
    {

        public int MatchCount { get; set; }

        public TitleModel? Title { get; set; }
    }

    public class ExactSearchResultModel
    {

        public string Url { get; set; } = string.Empty;
        public List<ArtistStringSearchResultModel> Persons { get; set; } = new List<ArtistStringSearchResultModel>();

        public List<TitleExactSearchResultModel> Titles { get; set; } = new List<TitleExactSearchResultModel>();
    }

    public class BestSearchResultModel
    {

        public string Url { get; set; } = string.Empty;
        public List<ArtistStringSearchResultModel> Persons { get; set; } = new List<ArtistStringSearchResultModel>();

        public List<TitleBestSearchResultModel> Titles { get; set; } = new List<TitleBestSearchResultModel>();
    }
}
