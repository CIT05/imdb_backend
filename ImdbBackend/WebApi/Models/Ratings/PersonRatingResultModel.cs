namespace WebApi.Models.Ratings;

public class PersonRatingResultModel
{
    public string? Url { get; set; } = string.Empty;

    public double? PersonRating { get; set; }

    public string? PersonUrl { get; set; } = string.Empty;
}
