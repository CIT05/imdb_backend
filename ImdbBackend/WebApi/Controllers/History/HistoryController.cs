using DataLayer.History;
using WebApi.Models.History;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace WebApi.Controllers.History
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : BaseController
    {
        private readonly IHistoryDataService _dataService;

        public HistoryController(IHistoryDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _dataService = dataService;
        }


        [HttpGet("search/{userId}")]
        [Authorize]
        public IActionResult GetSearchHistoryByUser(int userId)
        {
            try 
            {
            var searchHistory = _dataService.GetSearchHistoryByUser(userId);

            List<SearchHistoryModel> searchHistoryModels = searchHistory.Select(search => search.Adapt<SearchHistoryModel>()).ToList();

            return Ok(searchHistoryModels);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("search/phrase/{phrase}")]
        [Authorize]
        public IActionResult GetSearchHistoryByPhrase(string phrase)
        {
            try
            {
            var searchHistory = _dataService.GetSearchHistoryByPhrase(phrase);

            List<SearchHistoryModel> searchHistoryModels = searchHistory.Select(search => search.Adapt<SearchHistoryModel>()).ToList();

            return Ok(searchHistoryModels);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("rating/user/{userId}")]
        [Authorize]
        public IActionResult GetRatingHistoryByUser(int userId)
        {
            try {
            var ratingHistory = _dataService.GetRatingHistoryByUser(userId);

            List<RatingHistoryModel> ratingHistoryModels = ratingHistory.Select(rating => rating.Adapt<RatingHistoryModel>()).ToList();

            return Ok(ratingHistoryModels);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet("rating/{tConst}")]
        [Authorize]
        public IActionResult GetRatingHistoryByTConst(string tConst)
        {
            try 
            {
            var ratingHistory = _dataService.GetRatingHistoryByTConst(tConst);

            List<RatingHistoryModel> ratingHistoryModels = ratingHistory.Select(rating => rating.Adapt<RatingHistoryModel>()).ToList();

            return Ok(ratingHistoryModels);
            }
            catch
            {
                return Unauthorized();
            }
        }

    }
}




