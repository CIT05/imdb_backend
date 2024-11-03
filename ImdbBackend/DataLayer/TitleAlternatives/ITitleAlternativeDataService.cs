namespace DataLayer.TitleAlternatives
{
    public interface ITitleAlternativeDataService
    {
        List<TitleAlternative> GetTitleAlternatives(int pageSize, int pageNumber);
        TitleAlternative? GetTitleAlternative(int akasId);


        List<TitleAlternative> GetTitleAlternativesByType(string type);

        int NumberOfTitleAlternatives();

    }
}



