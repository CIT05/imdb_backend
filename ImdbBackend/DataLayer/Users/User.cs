using DataLayer.TitlePrincipals;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Users;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Username{ get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

}

public class CreatedUserId
{
    public int UserId { get; set; }
}
