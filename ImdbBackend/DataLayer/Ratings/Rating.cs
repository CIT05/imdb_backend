
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Ratings;

    public class Rating
    {
    [Key]
    public string TConst { get; set; } = string.Empty;

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

}
