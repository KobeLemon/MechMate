using MongoDB.Driver;
using MechMate.Models;
using MongoDB.Driver.Core.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechMate.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        public MongoDbService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _vehicles = database.GetCollection<Vehicle>("MyGarage");
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