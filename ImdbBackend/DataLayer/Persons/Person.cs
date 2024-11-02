using DataLayer.TitlePrincipals;
using DataLayer.PersonRoles;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.Persons;

public class Person
{
    [Key]
    public string NConst { get; set; }

    public string primaryName{ get; set; } = string.Empty;

    public string? birthYear { get; set; } = string.Empty;

    public string? deathYear { get; set; }

    public List<TitlePrincipal> TitlePrincipals { get; set; }
    public List<PersonRole> PersonRoles { get; set; }

}
