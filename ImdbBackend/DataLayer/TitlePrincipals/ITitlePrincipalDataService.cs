

namespace DataLayer.TitlePrincipals
{
    public interface ITitlePrincipalDataService
    {
        List<TitlePrincipal> GetTitlePrincipals(int pageSize, int pageNumber);
        List<TitlePrincipal> GetPrincipalsByTitleId(string tConst);
        int NumberOfTitlePrincipals();
    }
}



