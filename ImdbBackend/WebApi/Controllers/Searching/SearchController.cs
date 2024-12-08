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

   [HttpGet("title/{title}", Name = nameof(TitleSearchResult))]

    public IActionResult TitleSearchResult(string title)
    {
        var TitleSearchResult = _dataService.TitleSearchResult(title);
        if (TitleSearchResult.Count == 0)
        {
            return NotFound();
        }

        List<TitleStringSearchResultModel> stringSearchResultModel = TitleSearchResult.Select(result => AdaptTitleStringSearchToTitleStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }


    [HttpGet("exact/{stringSearch}", Name = nameof(ExactSearch))]
    public IActionResult ExactSearch(string stringSearch)
    {
        var exactSearchResult = _dataService.ExactSearch(stringSearch);
        if (exactSearchResult.Titles.Count == 0 || exactSearchResult.Persons.Count == 0)
        {
            return NotFound();
        }

        ExactSearchResultModel exactSearchResultModel = AdaptExactSearchResultToExactSearchResultModel(exactSearchResult, stringSearch);

        return Ok(exactSearchResultModel);
    }

    [HttpGet("best/{stringSearch}", Name = nameof(BestMatchSearch))]
    public IActionResult BestMatchSearch(string stringSearch)
    {
        var bestSearchResult = _dataService.BestSearch(stringSearch);
        if (bestSearchResult.Titles.Count == 0 || bestSearchResult.Persons.Count == 0)
        {
            return NotFound();
        }

        BestSearchResultModel bestSearchResultModel = AdaptBestSearchResultToBestSearchResultModel(bestSearchResult, stringSearch);
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

    [HttpGet("actor/{name}", Name = nameof(SearchCelebs))]

    public IActionResult SearchCelebs(string name)
    {      
        var SearchCelebs = _dataService.SearchCelebs(name);
        if (SearchCelebs.Count == 0)
        {
            return NotFound();
        }

        List<ActorStringSearchResultModel> stringSearchResultModel = SearchCelebs.Select(result => AdaptActorStringSearchToActorStringSearchResultModel(result)).ToList();

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

    private ExactSearchResultModel AdaptExactSearchResultToExactSearchResultModel(ExactSearchResult exactSearchResult, string stringSearch)
    {
        var exactSearchResultModel = exactSearchResult.Adapt<ExactSearchResultModel>();
        exactSearchResultModel.Url = GetUrl(nameof(ExactSearch), new { stringSearch }) ?? string.Empty;

        if (exactSearchResult.Titles.Count != 0)
        {
            exactSearchResultModel.Titles = exactSearchResultModel.Titles.Select((title, index) =>
            {
                var TConst = exactSearchResult.Titles.Select(t => t.TConst).FirstOrDefault();
                if(title.Title != null)
                {
                    title.Title.Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = TConst }) ?? string.Empty;
                }
                return title;
            }).ToList();
        }

        if(exactSearchResult.Persons.Count != 0)
        {
            exactSearchResultModel.Persons = exactSearchResultModel.Persons.Select((person, index) =>
            {
                var NConst = exactSearchResult.Persons.Select(p => p.Nconst).FirstOrDefault();
                if(person.Person != null)
                {
                    person.Person.Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = NConst }) ?? string.Empty;
                }
                return person;
            }).ToList();
        }

        return exactSearchResultModel;
    }

  private BestSearchResultModel AdaptBestSearchResultToBestSearchResultModel(BestSearchResult bestSearchResult, string stringSearch)
    {
        var bestSearchResultModel = bestSearchResult.Adapt<BestSearchResultModel>();
        bestSearchResultModel.Url = GetUrl(nameof(BestMatchSearch), new { stringSearch }) ?? string.Empty;

        if (bestSearchResult.Titles.Count != 0)
        {
            bestSearchResultModel.Titles = bestSearchResultModel.Titles.Select((title, index) =>
            {
                var TConst = bestSearchResult.Titles.Select(t => t.TConst).FirstOrDefault();
                if (title.Title != null)
                {
                    title.Title.Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = TConst }) ?? string.Empty;
                }
                return title;
            }).ToList();
        }

        if (bestSearchResult.Persons.Count != 0)
        {
            bestSearchResultModel.Persons = bestSearchResultModel.Persons.Select((person, index) =>
            {
                var NConst = bestSearchResult.Persons.Select(p => p.Nconst).FirstOrDefault();
                if(person.Person != null)
                {
                    person.Person.Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = NConst }) ?? string.Empty;
                }
                return person;
            }).ToList();
        }

        return bestSearchResultModel;
    }



}
