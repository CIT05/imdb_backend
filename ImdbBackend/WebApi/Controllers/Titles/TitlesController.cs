using DataLayer.Titles;
using WebApi.Models.Titles;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Ratings;
using Mapster;
using WebApi.Controllers.TitleAlternatives;
using WebApi.Controllers.TitlePrincipals;
using WebApi.Controllers.Persons;
using WebApi.Models.KnownFors;
using WebApi.Models.Production;
using DataLayer.KnownFors;
using DataLayer.Productions;
using WebApi.Models.Persons;
using WebApi.Models.TitlePrincipals;
using DataLayer.Persons;


namespace WebApi.Controllers.Titles;

[ApiController]
[Route("api/title")]

public class TitlesController : BaseController
{
    private readonly ITitleDataService _dataService;
    private readonly IPersonDataService _personDataService;

    public TitlesController(ITitleDataService dataService, IPersonDataService personDataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;
        _personDataService = personDataService;
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

        if (title.KnownFors != null)
        {
            titleModel.KnownFors = title.KnownFors.Select(kf => new KnownForModel
            {
                Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = kf.NConst }) ?? string.Empty,
                PrimaryName = _personDataService.GetPersonById(kf.NConst)?.PrimaryName ?? string.Empty
            }).ToList();
        }

        if (title.ProductionPersons != null && title.ProductionPersons.Count > 0)
        {
            titleModel.ProductionPersons = title.ProductionPersons
                .Where(pe => pe != null && pe.Person != null)  // Ensure pe and pe.Person are not null
                .Select(pe => new ProductionModel
                {
                    Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = pe.NConst }) ?? string.Empty,
                    RoleId = pe.RoleId,
                    PrimaryName = pe.Person.PrimaryName // Assuming pe.Person is now not null
                })
                .ToList();
        }

        if (title.Principals != null && title.Principals.Count > 0)
        {
            titleModel.Principals = title.Principals
                .Where(titlePrincipals => titlePrincipals != null && titlePrincipals.Person != null)  // Ensure titlePrincipals and titlePrincipals.Person are not null
                .Select(titlePrincipals => new TitlePrincipalDTO
                {
                    Url = GetUrl(nameof(TitlePrincipalController.GetTitlePrincipalsForATitle), new { tconst = title.TConst, nconst = titlePrincipals.NConst, ordering = titlePrincipals.Ordering, roleId = titlePrincipals.RoleId }),
                    Job = titlePrincipals.Job,
                    Characters = titlePrincipals.Characters,
                    Person = new PersonDTO
                    {
                        Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = titlePrincipals.NConst }),
                        PrimaryName = titlePrincipals.Person.PrimaryName // Assuming titlePrincipals.Person is now not null
                    }
                })
                .ToList();
        }

        return titleModel;
    }

}
