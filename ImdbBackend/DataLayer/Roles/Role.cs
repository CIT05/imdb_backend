using DataLayer.TitlePrincipals;
using System.Text.Json.Serialization;

namespace DataLayer.Roles;

    public class Role
    {
        public int RoleId { get; set; }
     
        public string RoleName { get; set; } = string.Empty;



    [JsonIgnore]
        public List<TitlePrincipal> TitlePrincipals { get; set; }

}
