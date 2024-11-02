using DataLayer.Titles;
using WebApi.Models.Titles;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace WebApi.Controllers.Titles;

[ApiController]
[Route("api/title")]

public class TitlesController(ITitleDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly ITitleDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetTitles))]
    public IActionResult GetTitles(int pageSize = 2, int pageNumber = 0)
    {
        var titles = _dataService.GetTitles(pageSize, pageNumber);

        var numberOfItmes = _dataService.NumberOfTitles();

        string linkName = nameof(GetTitles);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, titles);

        return Ok(result);
    }

    [HttpGet("{tconst}", Name = nameof(GetTitleById))]
    public IActionResult GetTitleById(string tconst)
    {
        var title = _dataService.GetTitleById(tconst);
        if (title == null)
        {
            return NotFound();
        }

        var titleModel = AdaptTitleToTitleModel(title);
        return Ok(titleModel);
    }

    private TitleModel AdaptTitleToTitleModel(Title title)
    {

        var titleModel = title.Adapt<TitleModel>();
        titleModel.Url = GetUrl(title.TConst);
        return titleModel;

    }

    private string? GetUrl(string tconst)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleById), new { tconst });
    }
}
