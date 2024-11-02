using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.TitleAlternatives
{
    public class TitleAlternative
    {
        [Key]
        public int AkasId { get; set; }
        public int Ordering { get; set; }
        public string? AltTitle { get; set; }
        public string? Region {  get; set; }
        public string? Language {  get; set; } = string.Empty;
        public string? Attributes { get; set; }
        public bool? IsOriginalTitle { get; set; }

        public string? TitleId { get; set; }


    [JsonIgnore] 
       public Title? Title { get; set; }


    }
}
