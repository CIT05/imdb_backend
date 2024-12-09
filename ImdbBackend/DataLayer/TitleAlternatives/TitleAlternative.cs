using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataLayer.Types;

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

        public string? TConst { get; set; }
        

        public List<TitleType> Types { get; set; } = new List<TitleType>();

    [JsonIgnore] 
       public Title? Title { get; set; }


    }
}
