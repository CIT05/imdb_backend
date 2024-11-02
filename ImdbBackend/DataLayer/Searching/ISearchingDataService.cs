namespace DataLayer.Searching;

public interface ISearchingDataService
{
    List<StringSearchResult> SearchTitles(string searchString, int userId);
}
