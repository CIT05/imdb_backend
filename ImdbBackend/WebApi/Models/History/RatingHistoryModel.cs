using WebApi.Models.Titles;

namespace WebApi.Models.History
{
    public class RatingHistoryModel : HistoryModel
    {
        public string TConst { get; set; }

        public int Value { get; set; }

        public TitleDTO? Title { get; set; }
    }
}