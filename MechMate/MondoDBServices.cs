using MongoDB.Driver;
using MechMate.Models;
using MongoDB.Driver.Core.Configuration;

namespace MechMate.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<MyGarage> _MyGarage;

        public MongoDbService(string config.ConnectionString, string config.databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _vehicles = database.GetCollection<MyGarage>("MyGarage");
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _vehicles.Find(_ => true).ToListAsync();
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _vehicles.InsertOneAsync(vehicle);
        }

        // more CRUD methods as needed
    }
}