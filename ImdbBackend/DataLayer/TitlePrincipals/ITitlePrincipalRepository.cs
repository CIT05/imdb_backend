

namespace DataLayer.TitlePrincipals
{
    public interface ITitlePrincipalRepository
    {
        List<TitlePrincipal> GetTitlePrincipals(int pageSize, int pageNumber);
        TitlePrincipal? GetRoleInTitle(string tconst, string nconst, int ordering, int roleId);
        int NumberOfTitlePrincipals();
    }
}



