using System.ComponentModel.DataAnnotations;

namespace DataLayer.Titles;

public class Title
{
    [Key]
    public string TConst { get; set; }

    public string titleType { get; set; } = string.Empty;

    public string primaryTitle { get; set; } = string.Empty;

    public string originalTitle { get; set; } = string.Empty;

    public bool isAdult { get; set; }

    public string? startYear { get; set; } = string.Empty;

    public int? endYear { get; set; }

    public int? runtimeMinutes { get; set; }

    public string? plot { get; set; }

    public string? poster { get; set; }


}
