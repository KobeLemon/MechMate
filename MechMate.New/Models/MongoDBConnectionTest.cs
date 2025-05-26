using MongoDB.Driver;

namespace MechMate.New.Models;

public class MongoDBConnectionTest
{
    private readonly MongoDBService _mongoDBService;

    public MongoDBConnectionTest(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    public async Task<bool> TestConnection()
    {
        try
        {
            var database = _mongoDBService.GetDatabase();
            await database.RunCommandAsync((MongoDB.Driver.Command<object>)"{ping:1}");
            return true;
        }
        catch
        {
            return false;
        }
    }
} 