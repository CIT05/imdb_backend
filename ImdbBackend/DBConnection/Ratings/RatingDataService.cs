using DataLayer.Ratings;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Ratings;

public class RatingDataService(string connectionString) : IRatingDataService
{
    private readonly string _connectionString = connectionString;

    public Rating? GetRatingById(string tconst)
    {
        var db = new ImdbContext(_connectionString);
        return db.Ratings.Find(tconst);

    }

    public List<Rating> GetRatings(int pageSize, int pageNumber)
    {
       var db = new ImdbContext(_connectionString);
        return db.Ratings.Skip(pageNumber * pageSize).Take(pageSize).ToList();
    }

    public List<RatingForUserResult> GetRatingByUser(int userId, string tconst)
    {
        var db = new ImdbContext(_connectionString);
        return db.RatingForUserResults.FromSqlInterpolated($"SELECT * FROM get_rating_by_user({userId}, {tconst})").ToList();
    }

    public bool AddRating(int userId, string tconst, decimal rating)
    {
        if(rating < 0 || rating > 10)
        {
            return false;
        }

        var db = new ImdbContext(_connectionString);
        var isAddRatingSuccess = db.AddRatingResults.FromSqlInterpolated($"SELECT * FROM add_rating({tconst}, {userId}, {rating})").ToList().FirstOrDefault().IsSuccess;
        return isAddRatingSuccess;
    }

    public List<PersonRatingResult> GetPersonRating(string nconst)
    {
        var db = new ImdbContext(_connectionString);
        return db.PersonRatingResults.FromSqlInterpolated($"SELECT * FROM get_rating_per_nconst({nconst})").ToList();
    }

    public int NumberOfRatings()
    {
        var db = new ImdbContext(_connectionString);
        return db.Ratings.Count();
    }
}
