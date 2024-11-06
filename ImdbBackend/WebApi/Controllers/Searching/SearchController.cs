using Microsoft.AspNetCore.Mvc;
using Mapster;
using DataLayer.Searching;
using WebApi.Controllers.Titles;
using WebApi.Models.Searching;
using WebApi.Controllers.Persons;
using Microsoft.AspNetCore.Authorization;



namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/search")]

public class SearchController(ISearchingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly ISearchingDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet("title/{userId}/{title}", Name = nameof(TitleSearchStringResult))]
    [Authorize]
    
    public IActionResult TitleSearchStringResult(int userId, string title)
    {
        try 
        {
        var titleSearchStringResult = _dataService.SearchTitles(title, userId);
        if (titleSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<TitleStringSearchResultModel> stringSearchResultModel = titleSearchStringResult.Select(result => AdaptTitleStringSearchToTitleStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpGet("title/exact/{stringSearch}", Name = nameof(ExactTitleSearch))]
    public IActionResult ExactTitleSearch(string stringSearch)
    {
        var exactSearchResult = _dataService.MovieExactSearch(stringSearch);
        if (exactSearchResult.Count == 0)
        {
            return NotFound();
        }

        List<ExactSearchResultModel> exactSearchResultModel = exactSearchResult.Select(result => AdaptExactSearchResultToExactSearchResultModel(result, stringSearch)).ToList();

        return Ok(exactSearchResultModel);
    }

    [HttpGet("title/best/{stringSearch}", Name = nameof(BestMatchTitleSearch))]
    public IActionResult BestMatchTitleSearch(string stringSearch)
    {
        var bestSearchResult = _dataService.MovieBestSearch(stringSearch);
        if (bestSearchResult.Count == 0)
        {
            return NotFound();
        }

        List<BestSearchResultModel> bestSearchResultModel = bestSearchResult.Select(result => AdaptBestSearchResultToBestSearchResultModel(result, stringSearch)).ToList();

        return Ok(bestSearchResultModel);
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
    [Authorize]
    public IActionResult ActorSearchStringResult(int userId, string name)
    {
        try 
        {
        var actorSearchStringResult = _dataService.SearchActors(name, userId);
        if (actorSearchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<ActorStringSearchResultModel> stringSearchResultModel = actorSearchStringResult.Select(result => AdaptActorStringSearchToActorStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
        }
        catch
        {
            return Unauthorized();
        }
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

    private ExactSearchResultModel AdaptExactSearchResultToExactSearchResultModel(ExactSearchResult exactSearchResult, string stringSearch)
    {
        var exactSearchResultModel = exactSearchResult.Adapt<ExactSearchResultModel>();
        exactSearchResultModel.Url = GetUrl(nameof(ExactTitleSearch), new { stringSearch }) ?? string.Empty;

        if(exactSearchResult.Title != null)
        {
            exactSearchResultModel.Title.Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = exactSearchResult.TConst }) ?? string.Empty;
        }
        return exactSearchResultModel;
    }

  private BestSearchResultModel AdaptBestSearchResultToBestSearchResultModel(BestSearchResult bestSearchResult, string stringSearch)
    {
        var bestSearchResultModel = bestSearchResult.Adapt<BestSearchResultModel>();
        bestSearchResultModel.Url = GetUrl(nameof(BestMatchTitleSearch), new { stringSearch }) ?? string.Empty;

        if (bestSearchResultModel.Title != null)
        {
            bestSearchResultModel.Title.Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = bestSearchResult.TConst }) ?? string.Empty;
        }

        return bestSearchResultModel;
    }



}
