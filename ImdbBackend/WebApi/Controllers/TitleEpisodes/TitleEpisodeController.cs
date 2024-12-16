using DataLayer.TitleEpisodes;
using WebApi.Models.TitleEpisodes;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Controllers.Titles;


namespace WebApi.Controllers.TitleEpisodes


{
    [ApiController]
    [Route("api/titleepisode")]

    public class TitleEpisodeController :  BaseController

    {
        private readonly ITitleEpisodeDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleEpisodeController(ITitleEpisodeDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetTitleEpisodes))]
        public IActionResult GetTitleEpisodes(int pageSize = 2, int pageNumber = 0)
        {
            var titleEpisodes = _dataService.GetTitleEpisodes(pageSize, pageNumber);
            var numberOfItems = _dataService.NumberOfTitleEpisodes();

            var titleEpisodeModels = new List<TitleEpisodeModel>();
            foreach (var titleEpisode in titleEpisodes)
            {
                titleEpisodeModels.Add(AdaptTitleEpisodeToTitleEpisodeModel(titleEpisode));
            }

            string linkName = nameof(GetTitleEpisodes);
            object result = CreatePaging(pageNumber, pageSize, numberOfItems, linkName, titleEpisodeModels);

            return Ok(result);
        }

        [HttpGet("{tconst}/{parentTconst}", Name = nameof(GetTitleEpisode))]
        public IActionResult GetTitleEpisode(string tconst, string parentTconst)
        {
            Console.WriteLine($"Received parameters: tconst={tconst}, parentTconst={parentTconst}");
            var titleEpisode = _dataService.GetTitleEpisode(tconst);
            if (titleEpisode == null)
            {
                return NotFound();
            }

            var titleEpisodeModel = AdaptTitleEpisodeToTitleEpisodeModel(titleEpisode);
            return Ok(titleEpisodeModel);
        }

        private TitleEpisodeModel AdaptTitleEpisodeToTitleEpisodeModel(TitleEpisode titleEpisode)
        {
            var titleEpisodeModel = titleEpisode.Adapt<TitleEpisodeModel>();
            if (titleEpisode.Tconst != null && titleEpisode.ParentTConst != null)
            {
                titleEpisodeModel.Url = GetUrl(titleEpisode.Tconst, titleEpisode.ParentTConst);
                titleEpisodeModel.ParentTitleUrl = GetUrl(nameof(TitlesController.GetTitleById), new {tconst = titleEpisodeModel.Tconst});
                titleEpisodeModel.TitleUrl = GetUrl(nameof(TitlesController.GetTitleById), new { tconst = titleEpisodeModel.ParentTConst });
            }

            return titleEpisodeModel;
        }

        private string? GetUrl(string tconst, string parentTconst)
        {
            var url = _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleEpisode), new { tconst, parentTconst });
            Console.WriteLine($"Generated URL: {url}");
            return url;
        }
    }
}
    