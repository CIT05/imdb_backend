using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private LinkGenerator _linkGenerator;
        public BaseController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        protected object CreatePaging<T>(int pageNumber, int pageSize, int total, string linkName, IEnumerable<T?> items)
        {
            const int maxPageSize = 10;

            pageSize = pageSize < maxPageSize ? maxPageSize : pageSize;

            var maxNumberOfPages = (int)Math.Ceiling(total / (double)pageSize);

            var curPage = GetLink(linkName, pageNumber, pageSize);
            var nextPage = pageNumber < maxNumberOfPages - 1 ? GetLink(linkName, pageNumber + 1, pageSize) : null;
            var prevPage = pageNumber > 0 ? GetLink(linkName, pageNumber - 1, pageSize) : null;

            var result = new
            {
                CurPage = curPage,
                NextPage = nextPage,
                PrevPage = prevPage,
                NumberOfItems = total,
                NumberPages = maxNumberOfPages,
                Items = items
            };
            return result;
        }

        protected string? GetLink(string linkName, int pageNumber, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                          HttpContext,
                          linkName,
                          new
                          {
                              pageNumber,
                              pageSize
                          });
        }
    }
}
