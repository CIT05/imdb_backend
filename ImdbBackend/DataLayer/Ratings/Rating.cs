
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Ratings;

public class Rating
{
    [Key]
    [ForeignKey("Title")]
    public string TConst { get; set; } = string.Empty;

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

}

public class RatingForUserResult
{
    public string TConst { get; set; } = string.Empty;
    
    public int UserId { get; set; } 

    public DateTime TimeStamp { get; set; }

    public double Rating { get; set; }

}

public class AddRatingResult
{
    public bool IsSuccess { get; set; }

}

public class PersonRatingResult
{
    public double PersonRating { get; set; }
}
