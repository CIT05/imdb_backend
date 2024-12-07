using DataLayer.Genres;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Genres
{
    public class GenreDataService(string connectionString) : IGenreDataService
    {
         private readonly string _connectionString = connectionString;

        public Genre? GetGenreById(int genreId)
        {
            var db = new ImdbContext(_connectionString);
            var genre = db.Genres.SingleOrDefault(g => g.GenreId == genreId);

            if(genre != null)
            {
                genre.Titles = db.Titles
                    .Where(t => t.Genres.Any(g => g.GenreId == genreId))
                    .Include(t => t.Rating)
                    .AsSplitQuery()
                    .ToList();
            }

            return genre;
        }

        public List<Genre> GetGenres(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Genres.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfGenres()
    {
        var db = new ImdbContext(_connectionString);
        return db.Genres.Count();
    }

    }
}