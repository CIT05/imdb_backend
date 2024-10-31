using DataLayer.TitlePrincipals;
using DBConnection.TitlePrincipalPrincipals;

namespace DBConnection.TitlePrincipals
{
    public class TitlePrincipalRepository(string connectionString) : ITitlePrincipalRepository
    {
        private readonly string _connectionString = connectionString;
        public TitlePrincipal? GetRoleInTitle(string tconst, string nconst, int ordering, int roleId)
        {
            var db = new TitlePrincipalContext(_connectionString);
        return db.TitlePrincipals.FirstOrDefault(title =>
        title.TConst == tconst &&
        title.NConst == nconst &&
        title.Ordering == ordering &&
        title.RoleId == roleId);
        }

        public List<TitlePrincipal> GetTitlePrincipals(int pageSize, int pageNumber)
        {
            var db = new TitlePrincipalContext(_connectionString);
            return db.TitlePrincipals.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfTitlePrincipals()
        {
            var db = new TitlePrincipalContext(_connectionString);
            return db.TitlePrincipals.Count();
        }
    }
}
