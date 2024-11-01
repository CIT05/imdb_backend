namespace DataLayer.Titles;

public interface ITitleDataService
{
    List<Title> GetTitles(int pageSize, int pageNumber);

    Title? GetTitleById(string titleId);

    int NumberOfTitles();
}
