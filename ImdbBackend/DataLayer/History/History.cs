using DataLayer.Users;

namespace DataLayer.History
{
    public class History
    {
        public int UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public User User { get; set; }

    }
}