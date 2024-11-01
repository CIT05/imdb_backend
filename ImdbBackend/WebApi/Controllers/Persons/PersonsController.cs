using DataLayer.Persons;
using WebApi.Models.Persons;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using DataLayer.Titles;
using WebApi.Controllers.Ratings;

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

    private PersonModel AdaptPersonToPersonModel(Person person)
    {

        var personModel = person.Adapt<PersonModel>();
        personModel.Url = GetUrl(nameof(GetPersonById), new { nconst = person.NConst });
        return personModel;

    }
}
