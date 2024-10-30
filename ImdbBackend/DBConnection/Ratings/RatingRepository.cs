using DataLayer.Ratings;
using DbConnection.Ratings;

namespace DBConnection.Ratings;

public class RatingRepository(string connectionString) : IRatingRepository
{
    private readonly string _connectionString = connectionString;

    public Rating? GetRatingById(string tconst)
    {
        var db = new RatingContext(_connectionString);
        return db.Ratings.Find(tconst);

    }

    public List<Rating> GetRatings(int pageSize, int pageNumber)
    {
       var db = new RatingContext(_connectionString);
        return db.Ratings.Skip(pageNumber * pageSize).Take(pageSize).ToList();
    }

    public int NumberOfRatings()
    {
        var db = new RatingContext(_connectionString);
        return db.Ratings.Count();
    }
}
