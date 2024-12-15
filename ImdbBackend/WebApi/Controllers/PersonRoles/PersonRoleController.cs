using DataLayer.PersonRoles;
using WebApi.Models.PersonRoles;
using WebApi.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Controllers.Roles;
using DataLayer.Roles;

namespace WebApi.Controllers.PersonRoles;

[ApiController]
[Route("api/personrole")]

public class PersonRoleController(IPersonRoleDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IPersonRoleDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetPersonRoles))]
    public IActionResult GetPersonRoles(int pageSize = 2, int pageNumber = 0)
    {
        var persons = _dataService.GetPersonRoles(pageSize, pageNumber);

        var numberOfItmes = _dataService.NumberOfPersonRoles();

        string linkName = nameof(GetPersonRoles);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, persons);

        return Ok(result);
    }

    [HttpGet("{nconst}", Name = nameof(GetRoleDetailsByPersonId))]
    public IActionResult GetRoleDetailsByPersonId(string nconst)
    {
        var personrole = _dataService.GetRoleDetailsByPersonId(nconst);
        if (personrole == null)
        {
            return NotFound();
        }

        var PersonRoleModel = AdaptPersonRolesToPersonRoleModels(personrole);
        return Ok(PersonRoleModel);
    }

    [HttpGet("{nconst}/{roleid}", Name = nameof(GetRoleImportanceOfPerson))]
    public IActionResult GetRoleImportanceOfPerson(string nconst, int roleid)
    {
        var importance = _dataService.GetRoleImportanceOfPerson(nconst, roleid);
          if(importance == null)
                 {
            return NotFound();
                    }
        var PersonRoleModel = AdaptPersonRoleToPersonRoleModel(importance);
            return Ok(PersonRoleModel);
    }

    private List<PersonRoleModel> AdaptPersonRolesToPersonRoleModels(IEnumerable<PersonRole> personRoles)
    {
        return personRoles.Select(role => {
            var roleModel = role.Adapt<PersonRoleModel>();
            roleModel.Url = GetUrl(role.NConst);

            if (roleModel.RoleId > 0)
            {
                roleModel.Role = new RoleModel
                {
                    Url = GetUrl(nameof(RolesController.GetRoleById), new { roleId = roleModel.RoleId }),
                };
            }

            return roleModel;
        }).ToList();
    }

    private PersonRoleModel AdaptPersonRoleToPersonRoleModel(PersonRole personRole)
    {
        var roleModel = personRole.Adapt<PersonRoleModel>();
        roleModel.Url = GetUrl(personRole.NConst);


        if (roleModel.RoleId > 0)
        {
            roleModel.Role = new RoleModel
            {
                Url = GetUrl(nameof(RolesController.GetRoleById), new { roleId = roleModel.RoleId }),
            };
        }

        return roleModel;
    }

    private string? GetUrl(string nconst)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetRoleDetailsByPersonId), new { nconst });
    }
}
