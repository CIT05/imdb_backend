using DataLayer.History;
using WebApi.Models.History;
using Mapster;
using Microsoft.AspNetCore.Mvc;



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

        [HttpGet("search")]
        public IActionResult GetSearchHistory()
        {
            var searchHistory = _dataService.GetSearchHistory();

            List<SearchHistoryModel> searchHistoryModels = searchHistory.Select(search => search.Adapt<SearchHistoryModel>()).ToList();

            return Ok(searchHistoryModels);
        }


        [HttpGet("search/{userId}")]
        public IActionResult GetSearchHistoryByUser(int userId)
        {
            var searchHistory = _dataService.GetSearchHistoryByUser(userId);

            List<SearchHistoryModel> searchHistoryModels = searchHistory.Select(search => search.Adapt<SearchHistoryModel>()).ToList();

            return Ok(searchHistoryModels);
        }

        [HttpGet("search/phrase/{phrase}")]
        public IActionResult GetSearchHistoryByPhrase(string phrase)
        {
            var searchHistory = _dataService.GetSearchHistoryByPhrase(phrase);

            List<SearchHistoryModel> searchHistoryModels = searchHistory.Select(search => search.Adapt<SearchHistoryModel>()).ToList();

            return Ok(searchHistoryModels);
        }


        [HttpGet("rating")]
        public IActionResult GetRatingHistory()
        {
            var ratingHistory = _dataService.GetRatingHistory();

            List<RatingHistoryModel> ratingHistoryModels = ratingHistory.Select(rating => rating.Adapt<RatingHistoryModel>()).ToList();

            return Ok(ratingHistoryModels);
        }

        [HttpGet("rating/user/{userId}")]
        public IActionResult GetRatingHistoryByUser(int userId)
        {
            var ratingHistory = _dataService.GetRatingHistoryByUser(userId);

            List<RatingHistoryModel> ratingHistoryModels = ratingHistory.Select(rating => rating.Adapt<RatingHistoryModel>()).ToList();

            return Ok(ratingHistoryModels);
        }

        [HttpGet("rating/{tConst}")]
        public IActionResult GetRatingHistoryByTConst(string tConst)
        {
            var ratingHistory = _dataService.GetRatingHistoryByTConst(tConst);

            List<RatingHistoryModel> ratingHistoryModels = ratingHistory.Select(rating => rating.Adapt<RatingHistoryModel>()).ToList();

            return Ok(ratingHistoryModels);
        }

    }
}




