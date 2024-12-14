using WebApi.Models.Types;

namespace WebApi.Models.TitleALternatives
{
    public class TitleAlternativeModel
    {
        public string? Url { get; set; }
        public int AkasId { get; set; }
        public int Ordering { get; set; }
        public string? AltTitle { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? Attributes { get; set; }
        public string? IsOriginalTitle { get; set; }
        public string? TConst { get; set; }

        public List<TypeModel> Types { get; set; }
    }}