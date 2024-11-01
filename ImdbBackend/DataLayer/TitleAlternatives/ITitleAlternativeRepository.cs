

namespace DataLayer.TitleAlternatives
{
    public interface ITitleAlternativeRepository
    {

        List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber);
        TitleAlternative? GetTitleAlternative(int akasId, int ordering);

        int NumberOfTitleAlternatives();

    

    }
}



