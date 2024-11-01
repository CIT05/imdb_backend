using DataLayer.Persons;
using WebApi.Models.Persons;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace WebApi.Controllers.Persons;

[ApiController]
[Route("api/person")]

public class PersonsController(IPersonDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IPersonDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetPersons))]
    public IActionResult GetPersons(int pageSize = 2, int pageNumber = 0)
    {
        var persons = _dataService.GetPersons(pageSize, pageNumber);

        var numberOfItmes = _dataService.NumberOfPersons();

        string linkName = nameof(GetPersons);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, persons);

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

    private PersonModel AdaptPersonToPersonModel(Person persons)
    {

        var personModel = persons.Adapt<PersonModel>();
        personModel.Url = GetUrl(persons.NConst);
        return personModel;

    }


    private string? GetUrl(string nconst)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetPersonById), new { nconst });
    }
}
