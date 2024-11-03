using DataLayer.TitlePrincipals;
using DataLayer.PersonRoles;
using DataLayer.KnownFors;
using DataLayer.Productions;
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
    public List<PersonRole> PersonRoles { get; set; }
    public List<KnownFor> KnownFors { get; set; }
    public List<Production> ProductionPersons { get; set; }

}

public class PersonsByMovieResult
{
    public string NConst { get; set; }

    public double PersonRating { get; set; }

    public Person? Person { get; set; }
}
