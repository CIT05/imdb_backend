using DataLayer.TitlePrincipals;
using WebApi.Models.TitlePrincipals;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using WebApi.Controllers.Persons;
using WebApi.Controllers.Roles;

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

        [HttpGet("{tconst}", Name = nameof(GetTitlePrincipalsForATitle))]
        public IActionResult GetTitlePrincipalsForATitle(string tconst)
        {
            var titlePrincipals = _dataService.GetPrincipalsByTitleId(tconst);
            if (titlePrincipals == null)
            {
                return NotFound();
            }

            List<TitlePrincipalModel> titlePrincipalModel = titlePrincipals.Select(titlePrinicpal => AdaptTitlePrincipalToTitlePrincipalModel(titlePrinicpal)).ToList();
            return Ok(titlePrincipalModel);
        }

        private TitlePrincipalModel AdaptTitlePrincipalToTitlePrincipalModel(TitlePrincipal titlePrincipal)
        {
            var titlePrincipalModel = titlePrincipal.Adapt<TitlePrincipalModel>();
            titlePrincipalModel.Url = GetUrl(nameof(GetTitlePrincipalsForATitle), new { tconst = titlePrincipal.TConst, nconst = titlePrincipal.NConst, ordering = titlePrincipal.Ordering, roleId = titlePrincipal.RoleId });
           
            if(titlePrincipalModel.Person != null)
            {
                titlePrincipalModel.Person.Url = GetUrl(nameof(PersonsController.GetPersonById), new { nconst = titlePrincipal.NConst });
            }

            if(titlePrincipalModel.Role != null)
            {
                titlePrincipalModel.Role.Url = GetUrl(nameof(RolesController.GetRoleById), new { roleId = titlePrincipal.RoleId });
            }

            
            return titlePrincipalModel;
        }

    }
}
