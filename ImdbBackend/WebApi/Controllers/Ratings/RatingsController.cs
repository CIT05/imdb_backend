using DataLayer.Ratings;
using WebApi.Models.Ratings;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace WebApi.Controllers.Ratings;

[ApiController]
[Route("api/rating")]

public class RatingsController(IRatingDataService dataService, LinkGenerator linkGenerator) : BaseController(linkGenerator)
{
    private readonly IRatingDataService _dataService = dataService;


    private readonly LinkGenerator _linkGenerator = linkGenerator;

    [HttpGet(Name = nameof(GetRatings))]
    public IActionResult GetRatings(int pageSize = 2, int pageNumber = 0)
    {
        var ratings = _dataService.GetRatings(pageSize, pageNumber);

        List<RatingModel> ratingModels = ratings.Select(rating => AdaptRatingToRatingModel(rating)).ToList();

        var numberOfItmes = _dataService.NumberOfRatings();

        string linkName = nameof(GetRatings);

        object result = CreatePaging(pageNumber, pageSize, numberOfItmes, linkName, ratingModels);

        return Ok(result);
    }

    [HttpGet("{tconst}", Name = nameof(GetRatingById))]
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

    private RatingModel AdaptRatingToRatingModel(Rating rating)
    {

        var ratingModel = rating.Adapt<RatingModel>();
        ratingModel.Url = GetUrl(nameof(GetRatingById), new {tconst = rating.TConst});
        return ratingModel;

    }



}
