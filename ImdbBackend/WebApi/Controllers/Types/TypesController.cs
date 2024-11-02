using DataLayer.Types;
using WebApi.Models.Types;
using Microsoft.AspNetCore.Mvc;
using Mapster;


namespace WebApi.Controllers.Types;

[ApiController]
[Route("api/type")]


public class TypesController(ITypeDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly ITypeDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetTypes))]

    public IActionResult GetTypes(int pageSize = 2, int pageNumber = 0)
    {
        var types = _dataService.GetTypes(pageSize, pageNumber);

        List<TypeModel> typeModels = types.Select(type => AdaptTypeToTypeModel(type)).ToList();

        var numberOfItmes = _dataService.NumberOfTypes();

        string linkName = nameof(GetTypes);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, typeModels);

        return Ok(result);
    }


[HttpGet("{typeid}", Name = nameof(GetTypeById))]
public IActionResult GetTypeById(int typeid)
{
    var type = _dataService.GetTypeById(typeid);
    if (type == null)
    {
        return NotFound();
    }

    var typeModel = AdaptTypeToTypeModel(type);
    return Ok(typeModel);
}




    private TypeModel AdaptTypeToTypeModel(TitleType type)
    {

        var typeModel = type.Adapt<TypeModel>();
        typeModel.Url = GetUrl(nameof(GetTypeById), new {typeid = type.TypeId});
        return typeModel;

    }

}