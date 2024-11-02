using DataLayer.Titles;
using WebApi.Models.Titles;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Ratings;
using Mapster;
using WebApi.Controllers.TitlePrincipals;
using WebApi.Models.TitlePrincipals;
using WebApi.Models.Persons;
using DataLayer.Persons;
using WebApi.Models.KnownFors;
using DataLayer.KnownFors;

namespace WebApi.Controllers.Titles;

[ApiController]
[Route("api/title")]

public class TitlesController : BaseController
{
    private readonly ITitleDataService _dataService;
    private readonly IKnownForDataService _knownForDataService;

    public TitlesController(ITitleDataService dataService, IKnownForDataService knownForDataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;
        _knownForDataService = knownForDataService;
    }


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
        titleModel.Url = GetUrl(nameof(GetTitleById), new { tconst = title.TConst });

        if (titleModel.Rating != null)
        {
            titleModel.Rating.Url = GetUrl(nameof(RatingsController.GetRatingById), new { tconst = title.TConst });
        }

        if (titleModel.Principals.Count > 0)
        {
            titleModel.Principals = titleModel.Principals.Select(
                    (principal, principalIndex) =>
                    {

                        var titlePrincipal = title.Principals.ElementAt(principalIndex);
                        principal.Url = GetUrl(nameof(TitlePrincipalController.GetTitlePrincipalsForATitle), new { tconst = titlePrincipal.TConst, nconst = titlePrincipal.NConst, ordering = titlePrincipal.Ordering, roleId = titlePrincipal.RoleId });
                        return principal;
                    }
            ).ToList();
    }
        if (title.KnownFors != null && title.KnownFors.Count > 0)
        {
            titleModel.KnownFors = title.KnownFors.Select(knownFor =>
            {
                string nameId = knownFor.NConst;
                var knownForEntity = _knownForDataService.GetKnownForByNameId(nameId);

                if (knownForEntity != null)
                {
                    var generatedUrl = GetUrl(
                        nameof(TitlesController.GetTitleById),
                        new { tconst = knownForEntity.TConst }
                    );
                    return new KnownForModel { Url = generatedUrl };
                }
                else
                {
                    Console.WriteLine($"No KnownFor found for title ID: {nameId}");
                    return new KnownForModel { Url = null };
                }
            }).ToList();
        }

        return titleModel; 
    }

}
