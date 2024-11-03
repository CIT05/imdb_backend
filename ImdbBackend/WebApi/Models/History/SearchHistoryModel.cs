namespace WebApi.Models.History
{
    public class SearchHistoryModel : HistoryModel
    {
        public int SearchId { get; set; }
        public string? Phrase { get; set; }
    }
}