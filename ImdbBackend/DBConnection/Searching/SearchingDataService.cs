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

    public ExactSearchResult ExactSearch(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var stringSearchArray = searchString.Split(" ").Select(s => s.ToLower()).ToArray();
        var movieResults = db.TitleExactSearchResult.FromSqlInterpolated($"SELECT * FROM exact_match_query({stringSearchArray})").Include(result => result.Title).ToList();
        var personsResults = db.ArtistStringSearchResult.FromSqlInterpolated($"SELECT * FROM exact_search_names({searchString})").Include(result => result.Person).ToList();

        var results = new ExactSearchResult
        {
            Titles = movieResults,
            Persons = personsResults
        };

        return results;
    }

    public BestSearchResult BestSearch(string searchString)
    {
        var db = new ImdbContext(_connectionString);
        var stringSearchArray = searchString.Split(" ").Select(s => s.ToLower()).ToArray();
        var movieResults = db.TitleBestSearchResult.FromSqlInterpolated($"SELECT * FROM exact_bestmatch_query({stringSearchArray})").Include(result => result.Title).ToList();
        var personsResults = db.ArtistStringSearchResult.FromSqlInterpolated($"SELECT * FROM best_search_names({searchString})").Include(result => result.Person).ToList();

        var results = new BestSearchResult
        {
            Titles = movieResults,
            Persons = personsResults
        };

        return results;
    }
}

