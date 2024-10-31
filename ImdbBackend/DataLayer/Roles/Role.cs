
using DataLayer.TitlePrincipals;

namespace DataLayer.Roles;

    public class Role
    {
        public int RoleId { get; set; }
     
        public string RoleName { get; set; } = string.Empty;

        public List<TitlePrincipal> TitlePrincipals { get; set; }

}
