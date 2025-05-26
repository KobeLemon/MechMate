using MongoDB.Driver;

namespace MechMate.New.Models;

public class MongoDBService
{
    private readonly IMongoDatabase _database;

    public MongoDBService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
} 