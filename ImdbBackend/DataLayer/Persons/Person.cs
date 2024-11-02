using DataLayer.TitlePrincipals;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Persons;

public class Person
{
    [Key]
    public string NConst { get; set; }

    public string PrimaryName{ get; set; } = string.Empty;

    public string? BirthYear { get; set; } = string.Empty;

    public string? DeathYear { get; set; }

    public List<TitlePrincipal> TitlePrincipals { get; set; }


}

public class PersonsByMovieResult
{
    public string NConst { get; set; }

    public double PersonRating { get; set; }

    public Person? Person { get; set; }
}
