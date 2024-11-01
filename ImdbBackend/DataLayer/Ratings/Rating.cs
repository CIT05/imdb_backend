
using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Ratings;

    public class Rating
    {
    [Key]
    public string TConst { get; set; } = string.Empty;

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

    //public Title Title { get; set; } --- it was giving me an error like this?? was telling me one to one is not correct relationship, and on diagram it is not one to one
    //public List<Title> Titles { get; set; } = new List<Title>();
    public Title Title { get; set; }

}
