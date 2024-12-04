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
    
     public List<TitleStringSearchResult> TitleSearchResult(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.TitleStringSearchResults.FromSqlInterpolated($"SELECT * FROM string_search_titles({searchString})").ToList();

        return results;
    }
    
    public List<TitleStringSearchResult> SearchTitlesByMultipleValues(string? titleMovie, string? moviePlot, string? titleCharacters, string? personName, int userId)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.TitleStringSearchResults.FromSqlInterpolated($"SELECT * FROM structured_string_search({titleMovie},{moviePlot}, {titleCharacters}, {personName}, {userId})").ToList();

        return results;
    }

    public List<ActorStringSearchResult> SearchCelebs(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var results = db.ActorStringSearchResults.FromSqlInterpolated($"SELECT * FROM name_search({searchString})").ToList();

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

    public List<ExactSearchResult> MovieExactSearch(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var stringSearchArray = searchString.Split(" ").Select(s => s.ToLower()).ToArray();
        var results = db.ExactSearchResults.FromSqlInterpolated($"SELECT * FROM exact_match_query({stringSearchArray})").Include(result => result.Title).ToList();

        return results;
    }

    public List<BestSearchResult> MovieBestSearch(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var stringSearchArray = searchString.Split(" ").Select(s => s.ToLower()).ToArray();
        var results = db.BestSearchResults.FromSqlInterpolated($"SELECT * FROM exact_bestmatch_query({stringSearchArray})").Include(result => result.Title).ToList();

        return results;
    }
}

