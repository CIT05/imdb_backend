using DataLayer.Ratings;
using WebApi.Models.Ratings;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Controllers.Titles;
using WebApi.Controllers.Users;
using WebApi.Controllers.Persons;

namespace WebApi.Controllers.Ratings;

[ApiController]
[Route("api/rating")]

public class RatingsController(IRatingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IRatingDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet("/movie", Name = nameof(GetRatings))]
    public IActionResult GetRatings(int pageSize = 2, int pageNumber = 0)
    {
        var ratings = _dataService.GetRatings(pageSize, pageNumber);

        List<RatingModel> ratingModels = ratings.Select(rating => AdaptRatingToRatingModel(rating)).ToList();

        var numberOfItmes = _dataService.NumberOfRatings();

        string linkName = nameof(GetRatings);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, ratingModels);

        return Ok(result);
    }

    [HttpGet("movie/{tconst}", Name = nameof(GetRatingById))]
    public IActionResult GetRatingById(string tconst)
    {
        var rating = _dataService.GetRatingById(tconst);
        if (rating == null)
        {
            return NotFound();
        }

        var ratingModel = AdaptRatingToRatingModel(rating);
        return Ok(ratingModel);
    }

    [HttpGet("movie/{userId}/{tconst}", Name = nameof(GetRatingByUser))]
    public IActionResult GetRatingByUser(int userId, string tconst)
    {
        var ratingResultList = _dataService.GetRatingByUser(userId, tconst);
        if (ratingResultList.Count == 0)
        {
            return NotFound();
        }

        List<RatingForUserResultModel> ratingForUserResultModel = ratingResultList.Select(ratingResult => AdaptRatingForUserResultToRatingForUserResultModel(ratingResult)).ToList();
        return Ok(ratingForUserResultModel);
    }

    [HttpPost("movie/{userId}/{tconst}/{ratingValue}")]
    public IActionResult AddRating(int userId, string tconst, decimal ratingValue)
    {
        var createdRating = _dataService.AddRating(userId, tconst, ratingValue);
        if (createdRating == null || createdRating == false)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpGet("person/{nconst}", Name = nameof(GetRatingByPerson))]
    public IActionResult GetRatingByPerson(string nconst)
    {
        var ratingResultList = _dataService.GetPersonRating(nconst);
        if (ratingResultList.Count == 0)
        {
            return NotFound();
        }

        List<PersonRatingResultModel> ratingForUserResultModel = ratingResultList.Select(ratingResult => AdaptPersonRatingResultToPersonRatingResultModel(ratingResult, nconst)).ToList();
        return Ok(ratingForUserResultModel);
    }
    private RatingModel AdaptRatingToRatingModel(Rating rating)
    {

        var ratingModel = rating.Adapt<RatingModel>();
        ratingModel.Url = GetUrl(nameof(GetRatingById), new {tconst = rating.TConst});
        return ratingModel;

    }

    private RatingForUserResultModel AdaptRatingForUserResultToRatingForUserResultModel(RatingForUserResult ratingForUserResult)
    {

        var ratingForUserResultModel = ratingForUserResult.Adapt<RatingForUserResultModel>();
        ratingForUserResultModel.Url = GetUrl(nameof(GetRatingByUser), new { userId = ratingForUserResult.UserId, tconst = ratingForUserResult.TConst });

        if (ratingForUserResult.TConst != null)
        {
            ratingForUserResultModel.TitleUrl = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = ratingForUserResult.TConst });
            ratingForUserResultModel.TitleOverallRatingUrl = GetUrl(nameof(RatingsController.GetRatingById), new { tconst = ratingForUserResult.TConst });
        }

        ratingForUserResultModel.UserUrl = GetUrl(nameof(UsersController.GetUserById), new { userid = ratingForUserResult.UserId });
     

        return ratingForUserResultModel;
    }

    private PersonRatingResultModel AdaptPersonRatingResultToPersonRatingResultModel(PersonRatingResult personRatingResult, string nconst)
    {
        var personRatingResultModel = personRatingResult.Adapt<PersonRatingResultModel>();
        personRatingResultModel.Url = GetUrl(nameof(GetRatingByPerson), new { nconst });
        personRatingResultModel.PersonUrl = GetUrl(nameof(PersonsController.GetPersonById), new { nconst });
        return personRatingResultModel;
    }




}
