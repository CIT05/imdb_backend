using DataLayer.Productions;

namespace DBConnection.Productions
{
    public class ProductionDataService(string connectionString) : IProductionDataService
    {
        private readonly string _connectionString = connectionString;
        public List<Production> GetProductionsByTitleIds(List<string> titleIds)
        {
            using var db = new ImdbContext(_connectionString);
            return db.Productions
                .Where(pp => titleIds.Contains(pp.TConst))
                .ToList();
        }
    }
}
