using Microsoft.AspNetCore.Mvc;
using Mapster;
using DataLayer.Searching;
using WebApi.Controllers.Titles;
using WebApi.Models.Searching;



namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/saerch")]

public class SearchController(ISearchingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly ISearchingDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet("{userId}/{searchString}", Name = nameof(SearchStringResult))]
    public IActionResult SearchStringResult(int userId, string searchString)
    {
        var searchStringResult = _dataService.SearchTitles(searchString, userId);
        if (searchStringResult.Count == 0)
        {
            return NotFound();
        }

        List<StringSearchResultModel> stringSearchResultModel = searchStringResult.Select(result => AdapStringSearchToStringSearchResultModel(result)).ToList();

        return Ok(stringSearchResultModel);
    }

    private StringSearchResultModel AdapStringSearchToStringSearchResultModel(StringSearchResult stringSearchResult)
    {

        var stringSearchResultModel = stringSearchResult.Adapt<StringSearchResultModel>();
        stringSearchResultModel.Url = GetUrl(nameof(TitlesController.GetTitleById), new {tconst = stringSearchResult.TitleId});
        return stringSearchResultModel;

    }



}
