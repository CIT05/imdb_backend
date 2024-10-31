using DataLayer.Titles;
using DataLayer.Persons;
using DataLayer.Roles;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.TitlePrincipals
{
    public class TitlePrincipal
    {
        [Key]
        public string TConst { get; set; }
        public string NConst { get; set; }
        public int RoleId { get; set; }
        public int Ordering {  get; set; }
        public string? Job {  get; set; }
        public string? Characters { get; set; }

        private Title Title { get; set; }
        private Person Person { get; set; }
        private Role Role {  get; set; }

    }
}
