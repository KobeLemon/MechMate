using MechMate.Interfaces;
using MechMate.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace MechMate.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(string connectionString, string databaseName)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== MongoDB Connection Attempt ===");
                System.Diagnostics.Debug.WriteLine($"Platform: {DeviceInfo.Platform}");
                System.Diagnostics.Debug.WriteLine($"Connection String Type: {(connectionString.StartsWith("mongodb+srv://") ? "SRV" : "Standard")}");

                var settings = MongoClientSettings.FromConnectionString(connectionString);

                // Essential Atlas settings
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                settings.UseTls = true;

                System.Diagnostics.Debug.WriteLine($"SSL: {settings.UseTls}");

                var client = new MongoClient(settings);
                _database = client.GetDatabase(databaseName);

                System.Diagnostics.Debug.WriteLine("MongoDB connection established successfully!");

            }
            catch (MongoAuthenticationException authEx)
            {
                System.Diagnostics.Debug.WriteLine($"Authentication failed: {authEx.Message}");
                throw new Exception($"MongoDB authentication failed. Check username/password. Details: {authEx.Message}", authEx);
            }
            catch (TimeoutException timeoutEx)
            {
                System.Diagnostics.Debug.WriteLine($"Connection timeout: {timeoutEx.Message}");
                throw new Exception($"MongoDB connection timed out. If using SRV format, try standard connection string. Details: {timeoutEx.Message}", timeoutEx);
            }
            catch (MongoConnectionException connEx)
            {
                System.Diagnostics.Debug.WriteLine($"Connection error: {connEx.Message}");
                throw new Exception($"Could not connect to MongoDB. Check connection string and network. Details: {connEx.Message}", connEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Full exception: {ex}");
                throw new Exception($"Failed to connect to MongoDB: {ex.Message}", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public async Task<List<Vehicle>> GetAllShortInfoForGarage()
        {
            IMongoCollection<Vehicle> collection = _database.GetCollection<Vehicle>("MyGarage");
            ProjectionDefinition<Vehicle, Vehicle> fields = Builders<Vehicle>.Projection.Expression(x => new Vehicle
            {
                Id = x.Id,
                Year = x.Year,
                Make = x.Make,
                Model = x.Model,
                VIN = x.VIN,
                ImageBase64 = x.ImageBase64,
            });
            var vehicles = await collection.Find(x => true).Project(fields).ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetSingleVehicle(string vehicleId)
        {
            IMongoCollection<Vehicle> collection = _database.GetCollection<Vehicle>("MyGarage");
            return await collection.Find(x => x.Id == vehicleId).FirstOrDefaultAsync();
        }

        public async Task<List<RepairInstance>> GetAllRepairsForSingleVehicle(string vehicleId)
        {
            IMongoCollection<RepairInstance> collection = _database.GetCollection<RepairInstance>("RepairInstance");
            return await collection.Find(x => x.VehicleId == vehicleId).ToListAsync();
        }
    }
}
