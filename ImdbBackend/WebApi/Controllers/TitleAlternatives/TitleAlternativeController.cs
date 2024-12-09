using DataLayer.TitleAlternatives;
using WebApi.Models.TitleALternatives;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Controllers.Types;


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

        [HttpGet("{akasId}", Name = nameof(GetTitleAlternative))]
        public IActionResult GetTitleAlternative(int akasId)
        {
            Console.WriteLine($"Received parameters: akasId={akasId}");
            var titleAlternative = _dataService.GetTitleAlternative(akasId);
            if (titleAlternative == null)
            {
                return NotFound();
            }

            var titleAlternativeModel = AdaptTitleAlternativeToTitleAlternativeModel(titleAlternative);
            return Ok(titleAlternativeModel);
        }



        [HttpGet("type/{type}", Name = nameof(GetTitleAlternativesByType))]
        public IActionResult GetTitleAlternativesByType(int type)
        {
            var titleAlternatives = _dataService.GetTitleAlternativesByType(type);
            var numberOfItems = titleAlternatives.Count;

            var titleAlternativeModels = new List<TitleAlternativeModel>();
            foreach (var titleAlternative in titleAlternatives)
            {
                titleAlternativeModels.Add(AdaptTitleAlternativeToTitleAlternativeModel(titleAlternative));
            }

            string linkName = nameof(GetTitleAlternativesByType);
            object result = CreatePaging(0, numberOfItems, numberOfItems, linkName, titleAlternativeModels);

            return Ok(result);
        }

        [HttpGet("title/{tconst}", Name = nameof(GetTitleAlternativeForTitle))]
        public IActionResult GetTitleAlternativeForTitle(string tconst)
        {
            var titleAlternatives = _dataService.GetTitleAlternativeForTitle(tconst);

            var titleAlternativeModels = new List<TitleAlternativeModel>();
            foreach (var titleAlternative in titleAlternatives)
            {
                titleAlternativeModels.Add(AdaptTitleAlternativeToTitleAlternativeModel(titleAlternative));
            }


            return Ok(titleAlternativeModels);
        }

        private TitleAlternativeModel AdaptTitleAlternativeToTitleAlternativeModel(TitleAlternative titleAlternative)
        {
            var titleAlternativeModel = titleAlternative.Adapt<TitleAlternativeModel>();
            titleAlternativeModel.Url = GetUrl(nameof(GetTitleAlternative), new {akasid = titleAlternative.AkasId});

            if(titleAlternativeModel.Types.Count > 0)
            {
                titleAlternativeModel.Types = titleAlternativeModel.Types.Select(type =>
                {
                    type.Url = GetUrl(nameof(TypesController.GetTypeById), new { typeid = type.TypeId });
                    return type;
                }
                ).ToList();

            }

            return titleAlternativeModel;
    }
    

    
    
    }
}