using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.TitleEpisodes
{
    public class TitleEpisode
    {
        [Key]
        public string? Tconst { get; set; }

        public string? ParentTConst { get; set; }

        public int SeasonNumber { get; set; }

        public int EpisodeNumber { get; set; }

        public Title? Title { get; set; }
    }
}