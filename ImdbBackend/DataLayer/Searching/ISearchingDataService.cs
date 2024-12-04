namespace DataLayer.Searching;

public interface ISearchingDataService
{
    List<TitleStringSearchResult> SearchTitles(string searchString, int userId);

    List<TitleStringSearchResult> TitleSearchResult(string searchString);

    List<TitleStringSearchResult> SearchTitlesByMultipleValues(string? titleMovie, string? moviePlot, string? titleCharacters, string? personName, int userId);

    List<ExactSearchResult> MovieExactSearch(string searchString);

    List<BestSearchResult> MovieBestSearch(string searchString);

    List<ActorStringSearchResult> SearchActors(string searchString, int userId);

    List<ActorStringSearchResult> SearchCelebs(string searchString);

    List<ActorStringSearchResult> SearchActorsByMultipleValues(string? titleMovie, string? moviePlot, string? titleCharacters, string? personName, int userId);

}
