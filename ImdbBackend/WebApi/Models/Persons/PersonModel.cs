using DataLayer.PersonRoles;
using WebApi.Models.PersonRoles;

namespace WebApi.Models.Persons
{
    public class PersonModel
    {
        public string? Url { get; set; }

        public string PrimaryName { get; set; } = string.Empty;

        public string? birthYear { get; set; }

        public string? deathYear { get; set; }
        public List<PersonRoleModel> PersonRoles { get; set; }

    }
}
