using WebApi.Models.TitleEpisodes;

namespace WebApi.Models.TitleEpisodes
{
    public class TitleEpisodeModel
    {
        public string? Url { get; set; }
        public string? Tconst { get; set; }
        public string? ParentTConst { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
    }
}