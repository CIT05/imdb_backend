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

public class PersonsByMovieResultWithPerson
{
    public string NConst { get; set; }

    public double PersonRating { get; set; }
}

public class  PersonByMovieResultWithPerson : PersonsByMovieResultWithPerson
{

    public Person Person { get; set; }
}
