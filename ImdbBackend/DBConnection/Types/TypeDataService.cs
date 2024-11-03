using DataLayer.Types;

namespace DBConnection.Type;


public class TypeDataService(string connectionString) : ITypeDataService
{
    private readonly string _connectionString = connectionString;

    public TitleType? GetTypeById(int typeId)
    {
        var db = new ImdbContext(_connectionString);
        return db.Types.Find(typeId);

    }

    public List<TitleType> GetTypes(int pageSize, int pageNumber)
    {
        var db = new ImdbContext(_connectionString);
        return db.Types.Skip(pageNumber * pageSize).Take(pageSize).ToList();
    }

    public int NumberOfTypes()
    {
        var db = new ImdbContext(_connectionString);
        return db.Types.Count();
    }
}