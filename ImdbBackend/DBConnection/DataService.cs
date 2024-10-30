using DataLayer;
using DbConnection;
using Microsoft.EntityFrameworkCore;


namespace DBConnection;

public class DataService : IDataService
{
    private readonly string _connectionString;

    public DataService(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public string TestMethod()
    {
        var db = new ImdbContext(_connectionString);
        bool canConnect = db.Database.CanConnect();
        string message = canConnect ? "We can connect" : "No Connection";

        return message;
    }
   


}
