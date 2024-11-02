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

    public List<TitleStringSearchResult> SearchTitles(string searchString, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.TitleStringSearchResults.FromSqlInterpolated($"SELECT * FROM string_search({searchString}, {userId})").ToList();

        return results;
    }

    public List<TitleStringSearchResult> SearchTitlesByMultipleValues(string? titleMovie, string? moviePlot, string? titleCharacters, string? personName, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.TitleStringSearchResults.FromSqlInterpolated($"SELECT * FROM structured_string_search({titleMovie},{moviePlot}, {titleCharacters}, {personName}, {userId})").ToList();

        return results;
    }

    public List<ActorStringSearchResult> SearchActors(string searchString, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.ActorStringSearchResults.FromSqlInterpolated($"SELECT * FROM name_string_search({searchString}, {userId})").ToList();

        return results;
    }

    public List<ActorStringSearchResult> SearchActorsByMultipleValues(string? titleMovie, string? moviePlot, string? titleCharacters, string? personName, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.ActorStringSearchResults.FromSqlInterpolated($"SELECT * FROM name_structured_string_search({titleMovie},{moviePlot}, {titleCharacters}, {personName}, {userId})").ToList();

        return results;
    }
}

