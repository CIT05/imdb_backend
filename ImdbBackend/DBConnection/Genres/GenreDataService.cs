using DataLayer.Genres;

namespace DBConnection.Genres
{
    public class GenreDataService(string connectionString) : IGenreDataService
    {
         private readonly string _connectionString = connectionString;

        public Genre? GetGenreById(int genreId)
        {
            var db = new ImdbContext(_connectionString);
            return db.Genres.Find(genreId);
        }

        public List<Genre> GetGenres(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.Genres.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfGenres()
    {
        var db = new ImdbContext(_connectionString);
        return db.Ratings.Count();
    }

    }
}