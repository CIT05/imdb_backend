
using WebApi.Models.Persons;
using WebApi.Models.Roles;
using WebApi.Models.Titles;

namespace WebApi.Models.TitlePrincipals
{
    public class TitlePrincipalModel
    {
        public string? Url { get; set; }
        public string? Job { get; set; }
        public string? Characters { get; set; }
        public PersonDTO? Person { get; set; }  
        public RoleModel? Role { get; set; }
    }
}
