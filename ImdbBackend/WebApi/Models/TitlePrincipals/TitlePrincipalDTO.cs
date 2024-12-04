using WebApi.Models.Persons;
using WebApi.Models.Roles;

namespace WebApi.Models.TitlePrincipals
{
    public class TitlePrincipalDTO
    {
        public string? Url { get; set; }
        public string? Job { get; set; }
        public string? Characters { get; set; }

        public string? PersonUrl { get; set; }
    }
}
