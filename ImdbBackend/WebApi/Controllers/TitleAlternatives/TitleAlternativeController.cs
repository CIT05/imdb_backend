using DataLayer.TitleAlternatives;
using WebApi.Models.TitleALternatives;
using Microsoft.AspNetCore.Mvc;
using Mapster;


namespace WebApi.Controllers.TitleAlternatives

{
    [ApiController]
    [Route("api/titlealternative")]

    public class TitleAlternativeController : BaseController
    {
        private readonly ITitleAlternativeDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitleAlternativeController(ITitleAlternativeDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetTitleAlternatives))]
        public IActionResult GetTitleAlternatives(int pageSize = 2, int pageNumber = 0)
        {
            var titleAlternatives = _dataService.GetTitleAlternatives(pageSize, pageNumber);
            var numberOfItems = _dataService.NumberOfTitleAlternatives();

            var titleAlternativeModels = new List<TitleAlternativeModel>();
            foreach (var titleAlternative in titleAlternatives)
            {
                titleAlternativeModels.Add(AdaptTitleAlternativeToTitleAlternativeModel(titleAlternative));
            }

            string linkName = nameof(GetTitleAlternatives);
            object result = CreatePaging(pageNumber, pageSize, numberOfItems, linkName, titleAlternativeModels);

            return Ok(result);
        }

        [HttpGet("{akasId}/{ordering}", Name = nameof(GetTitleAlternative))]
        public IActionResult GetTitleAlternative(int akasId, int ordering)
        {
            Console.WriteLine($"Received parameters: akasId={akasId}, ordering={ordering}");
            var titleAlternative = _dataService.GetTitleAlternative(akasId, ordering);
            if (titleAlternative == null)
            {
                return NotFound();
            }

            var titleAlternativeModel = AdaptTitleAlternativeToTitleAlternativeModel(titleAlternative);
            return Ok(titleAlternativeModel);
        }

        private TitleAlternativeModel AdaptTitleAlternativeToTitleAlternativeModel(TitleAlternative titleAlternative)
        {
            var titleAlternativeModel = titleAlternative.Adapt<TitleAlternativeModel>();
            // titleAlternativeModel.Url = GetUrl(titleAlternative.AkasId, titleAlternative.Ordering, titleAlternative.Title, titleAlternative.Region, titleAlternative.Language, titleAlternative.Attributes, titleAlternative.IsOriginalTitle);

            return titleAlternativeModel;
    }
    

    
    
    }
}