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
}
