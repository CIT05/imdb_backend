using DataLayer.Titles;
using DataLayer.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;

namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/user")]

public class UsersController(IUserDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IUserDataService _dataService = dataService;

    [HttpGet("{userid}", Name = nameof(GetUserById))]
    public IActionResult GetUserById(int userid)
    {
        User user = _dataService.GetUserById(userid);
        if (user == null)
        {
            return NotFound();
        }

        UserModel userModel = AdaptUserToUserModel(user);
        return Ok(userModel);
    }

    private UserModel AdaptUserToUserModel(User user)
    {

        var userModel = user.Adapt<UserModel>();
        userModel.Url = GetUrl(nameof(GetUserById), new { userid = user.UserId });
       
        return userModel;

    }
}
