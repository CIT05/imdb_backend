using DataLayer.KnownFors;
using DataLayer.Roles;
using System.Runtime.CompilerServices;

namespace DBConnection.KnownFors
{
    public class KnownForDataService(string connectionString) : IKnownForDataService
    {
        private readonly string _connectionString = connectionString;
        public KnownFor? GetKnownForByTitleId(string titleId)
        {
            using var db = new ImdbContext(_connectionString);
            return db.KnownFors
                .Where(k => k.TConst == titleId)
                .OrderBy(k => k.TConst) 
                .FirstOrDefault(); 
        }

        public KnownFor? GetKnownForByNameId(string nameId)
        {
            using var db = new ImdbContext(_connectionString);

            return db.KnownFors
                .Where(k => k.NConst == nameId) 
                .OrderBy(k => k.NConst) 
                .FirstOrDefault(); 
        }
    }
}
