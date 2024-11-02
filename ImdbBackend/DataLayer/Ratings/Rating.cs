
using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataLayer.Ratings;

public class Rating
{
    [Key]
    [ForeignKey("Title")]
    public string TConst { get; set; } = string.Empty;

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

}
