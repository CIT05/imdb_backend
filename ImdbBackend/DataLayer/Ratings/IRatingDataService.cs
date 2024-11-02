

namespace DataLayer.Ratings;

public interface IRatingDataService
{

    List<Rating> GetRatings(int pageSize, int pageNumber);

    Rating? GetRatingById(string tconst);

    List<RatingForUserResult> GetRatingByUser(int userId, string tconst);
    
    bool AddRating(int userId, string tconst, int rating);

    int NumberOfRatings();
}
