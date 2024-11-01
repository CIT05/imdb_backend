using DataLayer.TitleAlternatives;

namespace DBConnection.TitleAlternatives
{
    public class TitleAlternativeDataService : ITitleAlternativeDataService
    {
        private readonly ITitleAlternativeRepository _titleAlternativeRepository;

        public TitleAlternativeDataService(ITitleAlternativeRepository titleAlternativeRepository)
        {
            _titleAlternativeRepository = titleAlternativeRepository;
        }

        public List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber)
        {
            return _titleAlternativeRepository.GetTitleAlternatives(pageSize, pageNumber);
        }

        public TitleAlternative? GetTitleAlternative(int akasId, int ordering)  
        {
            return _titleAlternativeRepository.GetTitleAlternative(akasId, ordering);
        }

        public int NumberOfTitleAlternatives()  
        {
            return _titleAlternativeRepository.NumberOfTitleAlternatives();
        }
        }
}