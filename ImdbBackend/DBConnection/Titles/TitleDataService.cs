

using DataLayer.Ratings;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DBConnection.TitlePrincipals;

namespace DBConnection.Titles
{
    public class TitleDataService(ITitleRepository titleRepository, IRatingDataService ratingDataService, ITitlePrincipalDataService titlePrincipalDataService): ITitleDataService
    {

        private readonly ITitleRepository _titleRepository = titleRepository;
        private readonly IRatingDataService _ratingDataService = ratingDataService;
        private readonly ITitlePrincipalDataService _titlePrincipalDataService = titlePrincipalDataService;

        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
            // Step 1: Retrieve Titles from the repository
            List<Title> titles = _titleRepository.GetTitles(pageSize, pageNumber);

            // Step 2: Populate both Rating and TitlePrincipal data for each Title
            var titlesWithDetails = titles.Select(title =>
            {
                // Fetch and attach Rating data
                Rating titleRating = _ratingDataService.GetRatingById(title.TConst);
                if (titleRating != null)
                {
                    title.Rating = titleRating;
                }

                // Fetch and attach TitlePrincipal data
                List<TitlePrincipal> principals = _titlePrincipalDataService.GetPrincipalsByTitleId(title.TConst);
                if (principals != null)
                {
                    title.Principals = principals;
                }

                return title;
            }).ToList();

            return titlesWithDetails;
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
