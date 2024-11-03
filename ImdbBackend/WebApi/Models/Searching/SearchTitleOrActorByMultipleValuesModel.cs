namespace WebApi.Models.Searching
{
    public class SearchTitleOrActorByMultipleValuesModel
    {
        public string? TitleMovie { get; set; } = string.Empty;
        public string? MoviePlot { get; set; } = string.Empty;
        public string? TitleCharacters { get; set; } = string.Empty;
        public string? PersonName { get; set; } = string.Empty; 
        public int UserId { get; set; }
    }
}
