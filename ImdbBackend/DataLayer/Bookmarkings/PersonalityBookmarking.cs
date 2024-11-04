using DataLayer.Persons;

namespace DataLayer.Bookmarkings
{
    public class PersonalityBookmarking : Bookmarking
    {
        public string NConst { get; set; } = String.Empty;

        public int UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public Person Person { get; set; }
    }
}
