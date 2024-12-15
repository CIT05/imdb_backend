using DataLayer.Persons;
using WebApi.Models.Persons;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Models.Searching;
using WebApi.Controllers.Ratings;
using WebApi.Models.Titles;
using WebApi.Controllers.PersonRoles;
using DataLayer.PersonRoles;
using DataLayer.KnownFors;
using WebApi.Controllers.Titles;
using DBConnection.KnownFors;
using WebApi.Models.KnownFors;
using WebApi.Models.Roles;
using WebApi.Controllers.Roles;

namespace WebApi.Controllers.Persons;

[ApiController]
[Route("api/person")]

public class PersonsController : BaseController
{
    private readonly IPersonDataService _dataService;
    private readonly IKnownForDataService _knownForDataService;

    public PersonsController(IPersonDataService dataService, IKnownForDataService knownForDataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _knownForDataService = knownForDataService;
    }



    [HttpGet(Name = nameof(GetPersons))]
    public IActionResult GetPersons(int pageSize = 2, int pageNumber = 0)
    {
        var persons = _dataService.GetPersons(pageSize, pageNumber);

        var numberOfItmes = _dataService.NumberOfPersons();

        string linkName = nameof(GetPersons);

        List<PersonModel> personsModel = persons.Select(person => AdaptPersonToPersonModel(person)).ToList();

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, personsModel);

        return Ok(result);
    }

    [HttpGet("{nconst}", Name = nameof(GetPersonById))]
    public IActionResult GetPersonById(string nconst)
    {
        var persons = _dataService.GetPersonById(nconst);
        if (persons == null)
        {
            return NotFound();
        }

        var PersonModel = AdaptPersonToPersonModel(persons);
        return Ok(PersonModel);
    }

    [HttpGet("movie/{tconst}", Name = nameof(GetPersonsByMovie))]
    public IActionResult GetPersonsByMovie(string tconst)
    {
        var personsByMovieResult = _dataService.GetPersonsByMovie(tconst);
        if (personsByMovieResult.Count == 0)
        {
            return NotFound();
        }
        var personsByMovieResultModel = personsByMovieResult.Select(person => AdaptPersonsByMovieResultToPersonsByMovieResultModel(person, tconst)).ToList();
        return Ok(personsByMovieResultModel);
    }

    private PersonModel AdaptPersonToPersonModel(Person person)
    {
        var personModel = person.Adapt<PersonModel>();
        personModel.Url = GetUrl(nameof(GetPersonById), new { nconst = person.NConst });

        // Map PersonRoles
        if (personModel.PersonRoles != null && personModel.PersonRoles.Count > 0)
        {
            personModel.PersonRoles = personModel.PersonRoles.Select((personRoleModel, index) =>
            {
                var personRole = person.PersonRoles.ElementAt(index);

                personRoleModel.Url = GetUrl(nameof(PersonRoleController.GetRoleDetailsByPersonId), new { nconst = personRole.NConst });

                if (personRole.Role != null)
                {
                    personRoleModel.Role = new RoleModel
                    {
                        Url = GetUrl(nameof(RolesController.GetRoleById), new { roleId = personRole.Role.RoleId }),
                        RoleName = personRole.Role.RoleName
                    };
                }

                return personRoleModel;
            }).ToList();
        }

        if (person.KnownFors != null && person.KnownFors.Count > 0)
        {
            personModel.KnownFors = person.KnownFors.Select(knownFor =>
            {
                var title = knownFor.Title;

                return new KnownForModel
                {
                    Url = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = knownFor.TConst }),
                    Title = title != null
                        ? new TitlePosterDTO
                        {
                            TitleName = title.OriginalTitle ?? "Unknown Title",
                            Poster = title.Poster ?? "No Poster Available"
                        }
                        : null
                };
            }).ToList();
        }

        return personModel;
    }


    private PersonsByMovieResultModel AdaptPersonsByMovieResultToPersonsByMovieResultModel(PersonsByMovieResult personsByMovieResult, string tconst)
    {
        var personsByMovieResultModel = personsByMovieResult.Adapt<PersonsByMovieResultModel>();
        personsByMovieResultModel.Url = GetUrl(nameof(GetPersonsByMovie), new { tconst });

        personsByMovieResultModel.PersonRatingUrl = GetUrl(nameof(RatingsController.GetRatingByPerson), new { nconst = personsByMovieResult.NConst });

        if(personsByMovieResultModel.Person != null)
        {
            personsByMovieResultModel.Person.Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = personsByMovieResult.NConst });
        }

        return personsByMovieResultModel;
    }
}
