using WebApi.Models.Persons;

namespace WebApi.Models.Bookmarkings
{
    public class PersonalityBookmarkingModel : BookmarkingModel
    {
        public string NConst { get; set; }

        public PersonDTO? Person { get; set; }

    }
}
