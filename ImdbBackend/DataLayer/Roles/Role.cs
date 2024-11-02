
using DataLayer.PersonRoles;
using DataLayer.TitlePrincipals;
using System.Text.Json.Serialization;

namespace DataLayer.Roles;

    public class Role
    {
        public int RoleId { get; set; }
     
        public string RoleName { get; set; } = string.Empty;

        public List<TitlePrincipal> TitlePrincipals { get; set; }
        public List<PersonRole> PersonRoles { get; set; }

}
