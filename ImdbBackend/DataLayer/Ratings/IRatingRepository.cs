namespace DataLayer.Ratings;

public interface IRatingRepository
{
    
    List<Rating> GetRatings(int pageSize, int pageNumber);

    Rating? GetRatingById(string tconst);

    int NumberOfRatings();

}
