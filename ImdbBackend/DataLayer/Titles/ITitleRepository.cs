

namespace DataLayer.Titles;

public interface ITitleRepository
{
    List<Title> GetTitles(int pageSize, int pageNumber);

    Title? GetTitleById(string titleId);

    int NumberOfTitles();
}
