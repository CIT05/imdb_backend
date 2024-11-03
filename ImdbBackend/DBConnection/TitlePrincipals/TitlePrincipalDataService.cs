using DataLayer.TitlePrincipals;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.TitlePrincipals
{
    public class TitlePrincipalDataService(string connectionString) : ITitlePrincipalDataService
    {
        private readonly string _connectionString = connectionString;

        public List<TitlePrincipal> GetTitlePrincipals(int pageSize, int pageNumber)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitlePrincipals.Skip(pageNumber * pageSize).Take(pageSize).Include(tp => tp.Person).Include(tp => tp.Role).ToList();
        }

        public int NumberOfTitlePrincipals()
        {
            var db = new ImdbContext(_connectionString);
            return db.TitlePrincipals.Count();
        }

        public List<TitlePrincipal> GetPrincipalsByTitleId(string tConst)
        {
            var db = new ImdbContext(_connectionString);
            return db.TitlePrincipals
                .Where(principal => principal.TConst == tConst)
                .Include(tp => tp.Person)
                .Include(tp => tp.Role)
                .ToList();
        }
    }
}
