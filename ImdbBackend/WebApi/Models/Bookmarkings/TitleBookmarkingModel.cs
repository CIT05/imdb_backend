using WebApi.Models.Titles;

namespace WebApi.Models.Bookmarkings
{
    public class TitleBookmarkingModel : BookmarkingModel
    {
        public string TConst { get; set; }

        public TitlePosterDTO? Title { get; set; }

    }
}
