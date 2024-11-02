using DataLayer.TitlePrincipals;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.Persons;

public class Person
{
    [Key]
    public string NConst { get; set; }

    public string PrimaryName{ get; set; } = string.Empty;

    public string? BirthYear { get; set; } = string.Empty;

    public string? DeathYear { get; set; }


    [JsonIgnore]
    public List<TitlePrincipal> TitlePrincipals { get; set; }


}
