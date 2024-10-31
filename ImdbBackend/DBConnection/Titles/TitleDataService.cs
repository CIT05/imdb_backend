

using DataLayer.Ratings;
using DataLayer.Titles;

namespace DBConnection.Titles
{
    public class TitleDataService(ITitleRepository titleRepository, IRatingDataService ratingDataService): ITitleDataService
    {

        private readonly ITitleRepository _titleRepository = titleRepository;
        private readonly IRatingDataService _ratingDataService = ratingDataService;

        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
           List<Title> titles = _titleRepository.GetTitles(pageSize, pageNumber);
           
            var titlesWithRatings = titles.Select(title =>
            {
                Rating titleRating = _ratingDataService.GetRatingById(title.TConst);
                if(titleRating != null)
                {
                    title.Rating = titleRating;
                }
                return title;
            }).ToList();

            return titlesWithRatings;
        }

        public Title? GetTitleById(string tconst)
        {
            Title title = _titleRepository.GetTitleById(tconst);
            
            if(title != null)
            {
                Rating titleRating = _ratingDataService.GetRatingById(title.TConst);
                if (titleRating != null)
                {
                    title.Rating = titleRating;
                }
                return title;

            }
            return null;
        }

        public int NumberOfTitles()
        {
            return _titleRepository.NumberOfTitles();
        }
    }
}
