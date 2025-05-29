using MechMate.Interfaces;
using MechMate.Models;
using MongoDB.Driver;

namespace MechMate.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(string connectionString, string databaseName)
        {
            try
            {
                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
            }
            catch (Exception ex)
            {
                // Log the error appropriately
                throw new Exception("Failed to connect to MongoDB", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }


        public IEnumerable<Tuple<int, string>> GetAllShortInfoForGarage(string collectionName)
        {
            // Add a method to get all vehicles year/make/model & image for "My Garage" page
            throw new NotImplementedException();
        }


        public Vehicle GetSingleVehicle(string collectionName, string vehicleId)
        {
            // Add a method to get an individual vehicle by ID for "My Ride" page
            throw new NotImplementedException();
        }

        public IEnumerable<RepairInstance> GetAllRepairsForSingleVehicle(string collectionName, string vehicleId)
        {
            // Add a method to get all repairs for a vehicle by ID for "My Repairs" page
            throw new NotImplementedException();
        }
    }
}
