using DataLayer.Ratings;

namespace DBConnection.Ratings;

public class RatingDataService(IRatingRepository ratingRepository) : IRatingDataService
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;

    public Rating? GetRatingById(string tconst)
    {
        return _ratingRepository.GetRatingById(tconst);
    }

    public List<Rating> GetRatings(int pageSize, int pageNumber)
    {
        return _ratingRepository.GetRatings(pageSize, pageNumber);
    }

    public int NumberOfRatings()
    {
        return _ratingRepository.NumberOfRatings();
    }
}
