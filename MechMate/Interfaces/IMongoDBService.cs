using MechMate.Models;
using MongoDB.Driver;

namespace MechMate.Interfaces;

public interface IMongoDBService
{
    /// <summary>
    /// Gets a collection from the MongoDB database.
    /// </summary>
    /// <typeparam name="T">The type of the documents in the collection.</typeparam>
    /// <param name="collectionName">The name of the collection to retrieve.</param>
    /// <returns>An IMongoCollection of type T.</returns>
    IMongoCollection<T> GetCollection<T>(string collectionName);

    /// <summary>
    /// Get year/make/model/imagebase64 for all vehicles. Tuple is displayName(year/make/model) and imagebase64.
    /// </summary>
    /// <param name="collectionName">The name of the collection to retrieve.</param>
    /// <returns>An IEnumerable of Tuples{int, string}</returns>
    IEnumerable<Tuple<int, string>> GetAllShortInfoForGarage(string collectionName);

    /// <summary>
    /// Get a single vehicle by its ID.
    /// </summary>
    /// <param name="collectionName">The name of the collection to retrieve.</param>
    /// <param name="vehicleId">The Vehicle's ID</param>
    /// <returns>A single item of custom type Vehicle</returns>
    Vehicle GetSingleVehicle(string collectionName, string vehicleId);

    /// <summary>
    /// Retrieves all repair instances for a single vehicle by its ID.
    /// </summary>
    /// <param name="collectionName">The name of the collection to retrieve.</param>
    /// <param name="vehicleId">The Vehicle's ID</param>
    /// <returns>An IEnumerable of items of custom type RepairInstance</returns>
    IEnumerable<RepairInstance> GetAllRepairsForSingleVehicle(string collectionName, string vehicleId);
}
