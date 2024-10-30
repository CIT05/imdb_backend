

using DataLayer.Titles;

namespace DBConnection.Titles
{
    public class TitleDataService(ITitleRepository titleRepository): ITitleDataService
    {

        private readonly ITitleRepository _titleRepository = titleRepository;

        public List<Title> GetTitles(int pageSize, int pageNumber)
        {
           return _titleRepository.GetTitles(pageSize, pageNumber);
        }

        public Title? GetTitleById(string tconst)
        {
            return _titleRepository.GetTitleById(tconst);
        }

        public int NumberOfTitles()
        {
            return _titleRepository.NumberOfTitles();
        }
    }
}
