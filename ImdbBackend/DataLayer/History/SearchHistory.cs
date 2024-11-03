using DataLayer.Users;

namespace DataLayer.History
{
    public class SearchHistory : History
    {
        public int SearchId { get; set; }
        public int UserId { get; set; }
        public string? Phrase { get; set; }

        public DateTime Timestamp { get; set; }

        public User User { get; set; }

    }
}