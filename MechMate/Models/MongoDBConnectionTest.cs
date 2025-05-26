using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MechMate.Models
{
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
                // Try to get the database stats as a connection test
                var database = _mongoDBService.GetCollection<object>("test").Database;
                await database.RunCommandAsync<object>(new MongoDB.Bson.BsonDocument("ping", 1));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }
    }
} 