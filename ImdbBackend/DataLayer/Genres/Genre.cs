using System.ComponentModel.DataAnnotations;
using DataLayer.Titles;



namespace DataLayer.Genres
{
    public class Genre
    {
        [Key]

        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;

        public ICollection<Title> Titles { get; set; } = new List<Title>();
    }
}