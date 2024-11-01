using DataLayer.Titles;
using WebApi.Models.Titles;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Ratings;
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

        List<TitleModel> titlesModel = titles.Select(title => AdaptTitleToTitleModel(title)).ToList();

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, titlesModel);

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

        TitleModel titleModel = AdaptTitleToTitleModel(title);

        return Ok(titleModel);
    }

    private TitleModel AdaptTitleToTitleModel(Title title)
    {

        var titleModel = title.Adapt<TitleModel>();
        titleModel.Url = GetUrl(nameof(RatingsController.GetRatingById), new {tconst = title.TConst});
        if(titleModel.Rating != null)
        {
            titleModel.Rating.Url = GetUrl(nameof(RatingsController.GetRatingById), new { tconst = title.TConst });
        }
        return titleModel;

    }

}
