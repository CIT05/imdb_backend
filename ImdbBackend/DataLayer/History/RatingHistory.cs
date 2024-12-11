using DataLayer.Persons;
using DataLayer.Users;
using DataLayer.Titles;

namespace DataLayer.History
{
    public class RatingHistory : History
    {

        public string? TConst { get; set; }
        public int UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public int Value { get; set; }

        public User User { get; set; }

        public Title Title { get; set; }
    }
}