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

        public Title Title { get; set; }
        public Person Person { get; set; }
        public Role Role {  get; set; }

    }
}
