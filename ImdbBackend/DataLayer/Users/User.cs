using DataLayer.Bookmarkings;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Users;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Username{ get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public List<TitleBookmarking> TitleBookmarkings { get; set; }

    public List<PersonalityBookmarking> PersonalityBookmarkings { get; set; }

}

public class CreateUserResult
{
    public int UserId { get; set; }
}

public class UpdateUserResult
{
    public int UserId { get; set; }
}




