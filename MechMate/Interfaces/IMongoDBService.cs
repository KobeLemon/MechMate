using MechMate.Models;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace MechMate.Interfaces;

public interface IMongoDBService
{
    /// <summary>
    /// Get year/make/model/vin/imagebase64 for all vehicles.
    /// </summary>
    /// <returns>A List with items of type Vehicle</returns>
    Task<List<Vehicle>> GetAllShortInfoForGarage();

    /// <summary>
    /// Get a single vehicle by its ID.
    /// </summary>
    /// <param name="vehicleId">The Vehicle's ID</param>
    /// <returns>A single item of type Vehicle</returns>
    Task<Vehicle>GetSingleVehicle(string vehicleId);

    /// <summary>
    /// Retrieves all repair instances for a single vehicle by its ID.
    /// </summary>
    /// <param name="vehicleId">The Vehicle's ID</param>
    /// <returns>A List with items of type RepairInstance</returns>
    Task<List<RepairInstance>> GetAllRepairsForSingleVehicle(string vehicleId);
}
