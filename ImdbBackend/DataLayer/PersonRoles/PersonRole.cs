using DataLayer.Roles;
using DataLayer.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataLayer.PersonRoles
{
    public class PersonRole
    {
        [ForeignKey("Person")]
        public string NConst { get; set; }
        public int RoleId { get; set; }

        public int Ordering { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }

        [JsonIgnore]
        public Person Person { get; set; }
    }

}