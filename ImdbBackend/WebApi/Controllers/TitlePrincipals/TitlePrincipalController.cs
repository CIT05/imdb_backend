using DataLayer.TitlePrincipals;
using WebApi.Models.TitlePrincipals;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace WebApi.Controllers.TitlePrincipals
{
    [ApiController]
    [Route("api/titleprincipal")]
    public class TitlePrincipalController : BaseController
    {
        private readonly ITitlePrincipalDataService _dataService;
        private readonly LinkGenerator _linkGenerator;

        public TitlePrincipalController(ITitlePrincipalDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetTitlePrincipals))]
        public IActionResult GetTitlePrincipals(int pageSize = 2, int pageNumber = 0)
        {
            var titlePrincipals = _dataService.GetTitlePrincipals(pageSize, pageNumber);
            var numberOfItems = _dataService.NumberOfTitlePrincipals();

            var titlePrincipalModels = new List<TitlePrincipalModel>();
            foreach (var titlePrincipal in titlePrincipals)
            {
                titlePrincipalModels.Add(AdaptTitlePrincipalToTitlePrincipalModel(titlePrincipal));
            }

            string linkName = nameof(GetTitlePrincipals);
            object result = CreatePaging(pageNumber, pageSize, numberOfItems, linkName, titlePrincipalModels);

            return Ok(result);
        }

        [HttpGet("{tconst}/{nconst}/{ordering}/{roleId}", Name = nameof(GetRoleInTitle))]
        public IActionResult GetRoleInTitle(string tconst, string nconst, int ordering, int roleId)
        {
            Console.WriteLine($"Received parameters: tconst={tconst}, nconst={nconst}, ordering={ordering}, roleId={roleId}");
            var titlePrincipal = _dataService.GetRoleInTitle(tconst, nconst, ordering, roleId);
            if (titlePrincipal == null)
            {
                return NotFound();
            }

            var titlePrincipalModel = AdaptTitlePrincipalToTitlePrincipalModel(titlePrincipal);
            return Ok(titlePrincipalModel);
        }

        [HttpGet("{tconst}", Name = nameof(GetTitlePrincipalsForATitle))]
        public IActionResult GetTitlePrincipalsForATitle(string tconst)
        {
            var titlePrincipals = _dataService.GetPrincipalsByTitleId(tconst);
            if (titlePrincipals == null)
            {
                return NotFound();
            }

            var titlePrincipalModel = AdaptTitlePrincipalListToModelList(titlePrincipals);
            return Ok(titlePrincipalModel);
        }

        private TitlePrincipalModel AdaptTitlePrincipalToTitlePrincipalModel(TitlePrincipal titlePrincipal)
        {
            var titlePrincipalModel = titlePrincipal.Adapt<TitlePrincipalModel>();
            titlePrincipalModel.Url = GetUrl(titlePrincipal.TConst, titlePrincipal.NConst, titlePrincipal.Ordering, titlePrincipal.RoleId);

            return titlePrincipalModel;
        }

        private List<TitlePrincipalModel> AdaptTitlePrincipalListToModelList(IEnumerable<TitlePrincipal> titlePrincipals)
        {
            return titlePrincipals
                .Select(tp => {
                    var titlePrincipalModel = tp.Adapt<TitlePrincipalModel>();
                    titlePrincipalModel.Url = GetUrl(tp.TConst, tp.NConst, tp.Ordering, tp.RoleId); 
                    return titlePrincipalModel;
                })
                .ToList();
        }

        private string? GetUrl(string tconst, string nconst, int ordering, int roleId)
        {
            var url = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoleInTitle), new { tconst, nconst, ordering, roleId });
            Console.WriteLine($"Generated URL: {url}");
            return url;
        }
    }
}
