using System.ComponentModel.DataAnnotations;
using DataLayer.Ratings;

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

    public Rating Rating { get; set; }

}
