using DataLayer.Bookmarkings;
namespace WebApi.Controllers.Bookmarkings;

using DataLayer.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Bookmarkings;
using WebApi.Models.Users;


[ApiController]
[Route("api/bookmarking")]

public class BookmarkingControlller(IBookmarkingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{

    private readonly IBookmarkingDataService _dataService = dataService;

    // POST: api/bookmarking/personality
    [HttpPost("personality")]
    public IActionResult AddPersonalityBookmarking([FromBody] PersonalityBookmarkingModel model)
    {
        if (model == null)
            return BadRequest("Invalid data.");

        var result = _dataService.AddPersonalityBookmarking(model.UserId, model.NConst);

        return CreatedAtAction(nameof(GetPersonalitiesBookmarkedByUser), new { userId = model.UserId }, result);
    }

    // POST: api/bookmarking/title
    [HttpPost("title")]
    public IActionResult AddTitleBookmarking([FromBody] TitleBookmarkingModel model)
    {
        if (model == null)
            return BadRequest("Invalid data.");

        var result = _dataService.AddTitleBookmarking(model.UserId, model.TConst);

        return CreatedAtAction(nameof(GetTitlesBookmarkedByUsers), new { userId = model.UserId }, result);
    }

    // DELETE: api/bookmarking/personality
    [HttpDelete("personality")]
    public IActionResult DeletePersonalityBookmarking(int userId, string nconst)
    {
        try
        {
            _dataService.DeletePersonalityBookmarking(userId, nconst);
            // If the deletion is successful, return a NoContent (204) response
            return NoContent(); // or return Ok() if you prefer
        }
        catch (Exception ex)
        {
            // Log the exception (if you have logging set up) and return an appropriate error response
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // DELETE: api/bookmarking/title
    [HttpDelete("title")]
    public IActionResult DeleteTitleBookmarking(int userId, string titleId)
    {
        var result = _dataService.DeleteTitleBookmarking(userId, titleId);

        return Ok(result);
    }

    // GET: api/bookmarking/personality/user/{userId}
    [HttpGet("personality/user/{userId}")]
    public IActionResult GetPersonalitiesBookmarkedByUser(int userId)
    {
        var results = _dataService.GetPersonalitiesBookmarkedByUser(userId);
        return Ok(results);
    }

    // GET: api/bookmarking/title/user/{userId}
    [HttpGet("title/user/{userId}")]
    public IActionResult GetTitlesBookmarkedByUsers(int userId)
    {
        var results = _dataService.GetTitlesBookmarkedByUsers(userId);
        return Ok(results);
    }


}

