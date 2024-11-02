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

    public class ExactSearchResultModel
    {
        public string Url { get; set; } = string.Empty;

        public TitleModel? Title { get; set; }
    }

    public class BestSearchResultModel
    {
        public string Url { get; set; } = string.Empty;

        public int MatchCount { get; set; }

        public TitleModel? Title { get; set; }
    }
}
