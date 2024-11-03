

using DataLayer.Persons;
using DataLayer.Roles;
using DataLayer.Titles;
using System.Text.Json.Serialization;

namespace DataLayer.Productions
{
    public class Production
    {

        public string NConst {  get; set; }
        public string TConst { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public Person Person { get; set; }
        [JsonIgnore]
        public Title Title { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }
}
