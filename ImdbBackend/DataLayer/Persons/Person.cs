using System.ComponentModel.DataAnnotations;

namespace DataLayer.Persons;

public class Person
{
    [Key]
    public string NConst { get; set; }

    public string primaryName{ get; set; } = string.Empty;

    public string? birthYear { get; set; } = string.Empty;

    public string? deathYear { get; set; }

 

}
