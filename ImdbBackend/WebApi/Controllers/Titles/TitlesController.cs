using DataLayer.Titles;
using WebApi.Models.Titles;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Ratings;
using Mapster;
using WebApi.Controllers.TitleAlternatives;
using WebApi.Controllers.TitlePrincipals;
using WebApi.Controllers.TitlePrincipals;
using WebApi.Controllers.Persons;
using WebApi.Models.KnownFors;
using WebApi.Models.Production;
using DataLayer.KnownFors;
using DataLayer.Productions;


namespace WebApi.Controllers.Titles;

[ApiController]
[Route("api/title")]

public class TitlesController : BaseController
{
    private readonly ITitleDataService _dataService;
    private readonly IKnownForDataService _knownForDataService;
    private readonly IProductionDataService _productionDataService;

    public TitlesController(ITitleDataService dataService, IKnownForDataService knownForDataService, IProductionDataService productionDataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;
        _knownForDataService = knownForDataService;
        _productionDataService = productionDataService;
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

        if (titleModel.TitleAlternatives != null)
        {
            titleModel.TitleAlternatives.ForEach(alt => alt.Url = GetUrl(nameof(TitleAlternativeController.GetTitleAlternative), new { akasId = alt.AkasId, ordering = alt.Ordering }));
        }

        var knownForIds = title.KnownFors.Select(kf => kf.TConst).Distinct().ToList();
        var productionPersonIds = title.ProductionPersons.Select(pp => pp.TConst).Distinct().ToList();

        var knownForEntities = _knownForDataService.GetKnownForByTitleIds(knownForIds);
        var productionEntities = _productionDataService.GetProductionsByTitleIds(productionPersonIds);

        if (knownForEntities != null && knownForEntities.Count > 0)
        {
            titleModel.KnownFors = knownForEntities.Select(kf => new KnownForModel
            {
                Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = kf.NConst }),
            }).ToList();
        }

        if (productionEntities != null && productionEntities.Count > 0)
        {
            titleModel.ProductionPersons = productionEntities.Select(pe => new ProductionModel
            {
                Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = pe.NConst }),
                RoleId = pe.RoleId
            }).ToList();
        }

        if (titleModel.Principals != null && titleModel.Principals.Count > 0)
        {
            titleModel.Principals = titleModel.Principals.Select((principal, index) =>
            {
                var titlePrincipal = title.Principals.ElementAt(index);
                principal.Url = GetUrl(nameof(TitlePrincipalController.GetTitlePrincipalsForATitle),
                    new { tconst = titlePrincipal.TConst, nconst = titlePrincipal.NConst, ordering = titlePrincipal.Ordering, roleId = titlePrincipal.RoleId });
                principal.PersonUrl = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = titlePrincipal.NConst });
                return principal;
            }).ToList();
        }

        return titleModel;
    }
}
