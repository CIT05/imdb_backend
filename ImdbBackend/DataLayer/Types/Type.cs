using System.ComponentModel.DataAnnotations;
using DataLayer.TitleAlternatives;

namespace DataLayer.Types
{
    public class TitleType
    {
        [Key]
        public int TypeId { get; set; }

        public string? TypeName { get; set; }

     public ICollection<TitleAlternative> Titles { get; set; } = new List<TitleAlternative>();

    }
}