using DataLayer.Roles;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Models.Roles;


namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/role")]

public class RolesController(IRoleDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IRoleDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetRoles))]
    public IActionResult GetRoles(int pageSize = 2, int pageNumber = 0)
    {
        List<Role> roles = _dataService.GetRoles(pageSize, pageNumber);

        List<RoleModel> rolesModel = roles.Select(role => AdaptRoleToRoleModel(role)).ToList();

        int numberOfItmes = _dataService.NumberOfRoles();

        string linkName = nameof(GetRoles);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, rolesModel);

        return Ok(result);
    }

    [HttpGet("{roleId}", Name = nameof(GetRoleById))]
    public IActionResult GetRoleById(int roleId)
    {
        var role = _dataService.GetRoleById(roleId);
        if (role == null)
        {
            return NotFound();
        }

        var roleModel = AdaptRoleToRoleModel(role);
        return Ok(roleModel);
    }

    private RoleModel AdaptRoleToRoleModel(Role role)
    {

        var roleModel = role.Adapt<RoleModel>();
        roleModel.Url = GetUrl(nameof(GetRoleById), new {roleId = role.RoleId});
        return roleModel;

    }



}
