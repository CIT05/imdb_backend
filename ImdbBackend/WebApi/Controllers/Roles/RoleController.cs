using DataLayer.Roles;
using WebApi.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApi.Controllers.Roles;

[ApiController]
[Route("api/role")]

public class RoleController(IRoleDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IRoleDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetRoles))]
    public IActionResult GetRoles(int pageSize = 2, int pageNumber = 0)
    {
        var roles = _dataService.GetRoles(pageSize, pageNumber);

        var numberOfItmes = _dataService.NumberOfRoles();

        string linkName = nameof(GetRoles);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, roles);

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
        roleModel.Url = GetUrl(role.RoleId);
        return roleModel;

    }


    private string? GetUrl(int roleId)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetRoleById), new { roleId });
    }
}
