using WebApi.Models.Titles;

namespace WebApi.Models.Genres
{
    public class GenreModel
    {
        public int GenreId { get; set; }

        public string? GenreName { get; set; }

        public string? Url { get; set; }
        public List<TitleDTO>? Titles { get; set; }

    }
}