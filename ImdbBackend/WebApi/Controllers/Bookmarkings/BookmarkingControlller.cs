using DataLayer.Bookmarkings;
namespace WebApi.Controllers.Bookmarkings;

using DataLayer.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Bookmarkings;
using WebApi.Models.Users;


[ApiController]
[Route("api/bookmarking")]

public class BookmarkingController(IBookmarkingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{

    private readonly IBookmarkingDataService _dataService = dataService;

    [HttpPost("personality")]
    [Authorize]
    public IActionResult AddPersonalityBookmarking([FromBody] PersonalityBookmarkingModel model)
    {
        if (model == null)
            return BadRequest("Invalid data.");

        try
        {
            var result = _dataService.AddPersonalityBookmarking(model.UserId, model.NConst);
            return CreatedAtAction(nameof(GetPersonalitiesBookmarkedByUser), new { userId = model.UserId }, result);
        }
        catch 
        {
            return Unauthorized();
        }
    }

    [HttpPost("title")]
    [Authorize]
    public IActionResult AddTitleBookmarking([FromBody] TitleBookmarkingModel model)
    {
        if (model == null)
            return BadRequest("Invalid data.");

        try {
        var result = _dataService.AddTitleBookmarking(model.UserId, model.TConst);

        return CreatedAtAction(nameof(GetTitlesBookmarkedByUsers), new { userId = model.UserId }, result);
        }

        catch 
        {
            return Unauthorized();
        }
        }


    [HttpDelete("personality")]
    [Authorize]
    public IActionResult DeletePersonalityBookmarking(int userId, string nconst)
    {
        try
        {
            _dataService.DeletePersonalityBookmarking(userId, nconst);
            // If the deletion is successful, return a NoContent (204) response
            return NoContent(); // or return Ok() if you prefer
        }
        catch 
        {
            return Unauthorized();

        }
    }


    [HttpDelete("title")]
    [Authorize]
    public IActionResult DeleteTitleBookmarking(int userId, string titleId)
    {
        try 
        {
        var result = _dataService.DeleteTitleBookmarking(userId, titleId);
        return Ok(result);
        }

        catch 
        {
            return Unauthorized();
        }
    }

    [HttpGet("personality/user/{userId}")]
    [Authorize]
    public IActionResult GetPersonalitiesBookmarkedByUser(int userId)
    {
        try
        {
            var results = _dataService.GetPersonalitiesBookmarkedByUser(userId);
            return Ok(results);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Unauthorized();
        }
    }

    [HttpGet("title/user/{userId}")]
    [Authorize]
    public IActionResult GetTitlesBookmarkedByUsers(int userId)
    {

        try 
        {
        var results = _dataService.GetTitlesBookmarkedByUsers(userId);
        return Ok(results);
        }

        catch 
        {
            return Unauthorized();
        }
    }


}

