using DataLayer.PersonRoles;
using WebApi.Models.PersonRoles;

namespace WebApi.Models.Persons
{
    public class PersonModel
    {
        public string? Url { get; set; }

        public string PrimaryName { get; set; } = string.Empty;

        public string? BirthYear { get; set; }

        public string? DeathYear { get; set; }

        public List<PersonRoleModel> PersonRoles { get; set; }

    }
}
