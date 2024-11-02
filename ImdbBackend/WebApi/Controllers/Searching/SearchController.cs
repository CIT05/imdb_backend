using Microsoft.AspNetCore.Mvc;
using Mapster;
using DataLayer.Searching;
using WebApi.Controllers.Titles;
using WebApi.Models.Searching;
using WebApi.Controllers.Persons;



namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/search")]

public class SearchController(ISearchingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly ISearchingDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet("title/{userId}/{title}", Name = nameof(TitleSearchStringResult))]
    public IActionResult TitleSearchStringResult(int userId, string title)
    {
        var titleSearchStringResult = _dataService.SearchTitles(title, userId);
        if (titleSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<TitleStringSearchResultModel> stringSearchResultModel = titleSearchStringResult.Select(result => AdaptTitleStringSearchToTitleStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }

    [HttpPost("title", Name = nameof(SearchTitleByMultipleValues))]
    public IActionResult SearchTitleByMultipleValues([FromBody] SearchTitleOrActorByMultipleValuesModel body)
    {
        var titleSearchStringResult = _dataService.SearchTitlesByMultipleValues(body.TitleMovie, body.MoviePlot, body.TitleCharacters, body.PersonName, body.UserId);
        if (titleSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<TitleStringSearchResultModel> stringSearchResultModel = titleSearchStringResult.Select(result => AdaptTitleStringSearchToTitleStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }

    [HttpGet("actor/{userId}/{name}", Name = nameof(ActorSearchStringResult))]
    public IActionResult ActorSearchStringResult(int userId, string name)
    {
        var actorSearchStringResult = _dataService.SearchActors(name, userId);
        if (actorSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<ActorStringSearchResultModel> stringSearchResultModel = actorSearchStringResult.Select(result => AdaptActorStringSearchToActorStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }

    [HttpPost("actor", Name = nameof(SearchActorByMultipleValues))]
    public IActionResult SearchActorByMultipleValues([FromBody] SearchTitleOrActorByMultipleValuesModel body)
    {
        var actorSearchStringResult = _dataService.SearchActorsByMultipleValues(body.TitleMovie, body.MoviePlot, body.TitleCharacters, body.PersonName, body.UserId);
        if (actorSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<ActorStringSearchResultModel> stringSearchResultModel = actorSearchStringResult.Select(result => AdaptActorStringSearchToActorStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }

    private TitleStringSearchResultModel AdaptTitleStringSearchToTitleStringSearchResultModel(TitleStringSearchResult titleStringSearchResult)
    {
        var stringSearchResultModel = titleStringSearchResult.Adapt<TitleStringSearchResultModel>();
        stringSearchResultModel.Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = titleStringSearchResult.TitleId }) ?? string.Empty;
        return stringSearchResultModel;
    }

    private ActorStringSearchResultModel AdaptActorStringSearchToActorStringSearchResultModel(ActorStringSearchResult actorStringSearchResult)
    {
        var stringSearchResultModel = actorStringSearchResult.Adapt<ActorStringSearchResultModel>();
        stringSearchResultModel.Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = actorStringSearchResult.ActorId }) ?? string.Empty;
        return stringSearchResultModel;
    }



}
