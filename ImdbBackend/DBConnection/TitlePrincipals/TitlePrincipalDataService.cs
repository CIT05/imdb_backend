using DataLayer.TitlePrincipals;

namespace DBConnection.TitlePrincipals
{
    public class TitlePrincipalDataService : ITitlePrincipalDataService
    {
        private readonly ITitlePrincipalRepository _titlePrincipalRepository;

        public TitlePrincipalDataService(ITitlePrincipalRepository titlePrincipalRepository)
        {
            _titlePrincipalRepository = titlePrincipalRepository;
        }

        public List<TitlePrincipal> GetTitlePrincipals(int pageSize, int pageNumber)
        {
            return _titlePrincipalRepository.GetTitlePrincipals(pageSize, pageNumber);
        }

        public TitlePrincipal? GetRoleInTitle(string tconst, string nconst, int ordering, int roleId)  
        {
            return _titlePrincipalRepository.GetRoleInTitle(tconst, nconst, ordering, roleId);
        }

        public int NumberOfTitlePrincipals()  
        {
            return _titlePrincipalRepository.NumberOfTitlePrincipals();
        }
    }
}
