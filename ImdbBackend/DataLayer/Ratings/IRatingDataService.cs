

namespace DataLayer.Ratings;

public interface IRatingDataService
{

    List<Rating> GetRatings(int pageSize, int pageNumber);

    Rating? GetRatingById(string tconst);

    int NumberOfRatings();
}
