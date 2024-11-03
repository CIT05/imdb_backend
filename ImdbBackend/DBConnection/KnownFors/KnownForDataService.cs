using DataLayer.KnownFors;
using DataLayer.Roles;
using System.Runtime.CompilerServices;

namespace DBConnection.KnownFors
{
    public class KnownForDataService(string connectionString) : IKnownForDataService
    {
        private readonly string _connectionString = connectionString;
        public List<KnownFor> GetKnownForByTitleIds(List<string> titleIds)
        {
            using var db = new ImdbContext(_connectionString);
            return db.KnownFors
                .Where(kf => titleIds.Contains(kf.TConst))
                .ToList();
        }

        public List<KnownFor> GetKnownForByNameId(string nameId)
        {
            using var db = new ImdbContext(_connectionString);

            return db.KnownFors
                .Where(k => k.NConst == nameId) 
                .OrderBy(k => k.NConst)
                .ToList();
        }
    }
}
