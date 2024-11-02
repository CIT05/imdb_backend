namespace DataLayer.TitleAlternatives
{
    public interface ITitleAlternativeDataService
    {
        List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber);
        TitleAlternative? GetTitleAlternative(int akasId, int ordering);

        int NumberOfTitleAlternatives();

    }
}



