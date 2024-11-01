using DataLayer.Titles;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.TitleAlternatives
{
    public class TitleAlternative
    {
        [Key]
        public int AkasId { get; set; }
        public int Ordering { get; set; }
        public string? AltTitle { get; set; }
        public string? Region {  get; set; }
        public string? Language {  get; set; }
        public string? Attributes { get; set; }
        public bool? IsOriginalTitle { get; set; }

        public string? TitleId { get; set; }

        private Title Title { get; set; }


    }
}
