using WebApi.Models.History;
using WebApi.Models.Bookmarkings;


namespace WebApi.Models.Users
{
    public class UserModel
    {
        public string? Url { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;


        public List<RatingHistoryModel> RatingHistory { get; set; } = new List<RatingHistoryModel>();
        public List<SearchHistoryModel> SearchHistory { get; set; } = new List<SearchHistoryModel>();

        public List<TitleBookmarkingModel> TitleBookmarkings { get; set; } = new List<TitleBookmarkingModel>();
        public List<PersonalityBookmarkingModel> PersonalityBookmarkings { get; set; } = new List<PersonalityBookmarkingModel>();

    }
}
