using System.Text.Json;
using MechMate.Models;
using Microsoft.Extensions.Logging;

namespace MechMate.Services;

// Service class for JSON file operations
public class FileService
{
    public FileService()
    {
    }

    // Write JSON data to file
    public async Task<bool> WriteJsonAsync<T>(List<T> newData, string fileName)
    {
        try
        {
            // Get the app's local data directory
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            string jsonString = JsonSerializer.Serialize(newData);
            await File.WriteAllTextAsync(filePath, jsonString);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error writing JSON: {ex.Message}");
            return false;
        }
    }

    // Read JSON list data from file
    public async Task<List<T>?> ReadJsonListAsync<T>(string fileName) where T : class
    {
        try
        {
            var json = await FileSystem.OpenAppPackageFileAsync(fileName);
            var jsonDocument = JsonDocument.Parse(json);
            return JsonSerializer.Deserialize<List<T>>(jsonDocument);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error reading JSON: {ex.Message}");
            return new List<T>();
        }
    }
}