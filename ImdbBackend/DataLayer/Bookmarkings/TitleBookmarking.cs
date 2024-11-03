using DataLayer.Titles;

namespace DataLayer.Bookmarkings
{
    public class TitleBookmarking : Bookmarking
    {
        public string TConst { get; set; }
        public int UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public Title Title { get; set; }
    }
}
