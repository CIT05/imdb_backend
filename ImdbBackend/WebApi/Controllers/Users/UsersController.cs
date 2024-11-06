using DataLayer.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Models.Users;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.Users;

[ApiController]
[Route("api/user")]

public class UsersController(IUserDataService dataService,Hashing hashing, IConfiguration configuration, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IUserDataService _dataService = dataService;
    private readonly Hashing _hashing = hashing;


    [HttpGet("id/{userid}", Name = nameof(GetUserById))]
    public IActionResult GetUserById(int userid)
    {
        User user = _dataService.GetUserById(userid)!;
        if (user == null)
        {
            return NotFound();
        }

        UserModel userModel = AdaptUserToUserModel(user);
        return Ok(userModel);
    }

    [HttpGet("{username}", Name = nameof(GetUserByName))]
    public IActionResult GetUserByName(string username)
    {
        User? user = _dataService.GetUserByName(username);
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
        if (_dataService.GetUserByName(createUserModel.Username) != null) {
            return BadRequest();
        }
        if (string.IsNullOrEmpty(createUserModel.Password))
        {
            return BadRequest();
        }
    
            (var hashedPwd, var salt) = _hashing.Hash(createUserModel.Password);

            var createdUser = _dataService.CreateUser(createUserModel.Username, hashedPwd, createUserModel.Language, salt);
            if (createdUser == null)
            {
                return BadRequest("User creation failed. Please check the input data.");
            }

            UserModel createdUserModel = AdaptUserToUserModel(createdUser);
            return Ok(createdUserModel);
     
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserModel model)
    {
        var user = _dataService.GetUserByName(model.Username);

        if (user == null)
        {
            return BadRequest();
        }
       if( !_hashing.Verify(model.Password, user.Password, user.Salt))
        {
            return BadRequest();
        }
        var claims = new List<Claim>
       {
           new Claim(ClaimTypes.Name, user.Username),
       };
        var secret = configuration.GetSection("Auth:Secret").Value;
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(430000),
            signingCredentials: creds
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new {username = user.Username, token = jwt});

    }



    [HttpDelete("{userid}")]
    [Authorize]
    public IActionResult DeleteUser(int userid)
    {
        try
        {
            bool deleted = _dataService.DeleteUser(userid);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpPut("{userid}")]
    [Authorize]
    public IActionResult UpdateUser(int userid, CreateUserModel createUserModel)
    {
        try {
        var updatedUser = _dataService.UpdateUser(userid, createUserModel.Username, createUserModel.Password, createUserModel.Language);
        if (updatedUser == null)
        {
            return NotFound();
        }
        UserModel updatedUserModel = AdaptUserToUserModel(updatedUser);
        return Ok(updatedUserModel);
        }
        catch
        {
            return Unauthorized();
        }
    }

    private UserModel AdaptUserToUserModel(User user)
    {

        var userModel = user.Adapt<UserModel>();
        userModel.Url = GetUrl(nameof(GetUserById), new { userid = user.UserId });
       
        return userModel;

    }
}
