using System.ComponentModel.DataAnnotations;
using DataLayer.Ratings;
using DataLayer.TitlePrincipals;
using DataLayer.TitleAlternatives;
using DataLayer.TitleEpisodes;
using DataLayer.Genres;

namespace DataLayer.Titles;

public class Title
{
    [Key]
    public string TConst { get; set; }

    public string TitleType { get; set; } = string.Empty;

    public string PrimaryTitle { get; set; } = string.Empty;

    public string OriginalTitle { get; set; } = string.Empty;

    public bool IsAdult { get; set; }

    public string? StartYear { get; set; }

    public string? EndYear { get; set; }

    public int? RuntimeMinutes { get; set; }

    public string? Plot { get; set; }

    public string? Poster { get; set; }
//had to add this here cause it was complaining about the relationship rating title?? on getTitles method
    //public string? RatingTConst { get; set; }
    public List<TitleAlternative> TitleAlternatives { get; set; } = new List<TitleAlternative>();
    public Rating Rating { get; set; }
    public List<TitlePrincipal> Principals { get; set; } = new List<TitlePrincipal>();

    public List<TitleEpisode> Episodes { get; set; } = new List<TitleEpisode>();

    public ICollection<Genre> Genres { get; set; } = new List<Genre>();


}
