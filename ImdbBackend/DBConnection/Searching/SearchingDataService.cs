using DataLayer.Searching;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.Searching;

public class SearchingDataService : ISearchingDataService
{
    private readonly string _connectionString;

    public SearchingDataService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<StringSearchResult> SearchTitles(string searchString, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.StringSearchResults.FromSqlInterpolated($"SELECT * FROM string_search({searchString}, {userId})").ToList();

        return results;
    }
}

