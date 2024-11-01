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

    [HttpPost]
    public IActionResult CreateUser(CreateUserModel createUserModel)
    {
        var createdUser = _dataService.CreateUser(createUserModel.Username, createUserModel.Password, createUserModel.Language);
        if(createdUser == null)
        {
            return BadRequest();
        }
        UserModel createdUserModel = AdaptUserToUserModel(createdUser);
        return Ok(createdUserModel);
    }

    [HttpDelete("{userid}")]
    public IActionResult DeleteUser(int userid)
    {
        bool deleted = _dataService.DeleteUser(userid);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{userid}")]
    public IActionResult UpdateUser(int userid, CreateUserModel createUserModel)
    {
        var updatedUser = _dataService.UpdateUser(userid, createUserModel.Username, createUserModel.Password, createUserModel.Language);
        if (updatedUser == null)
        {
            return NotFound();
        }
        UserModel updatedUserModel = AdaptUserToUserModel(updatedUser);
        return Ok(updatedUserModel);
    }

    private UserModel AdaptUserToUserModel(User user)
    {

        var userModel = user.Adapt<UserModel>();
        userModel.Url = GetUrl(nameof(GetUserById), new { userid = user.UserId });
       
        return userModel;

    }
}
