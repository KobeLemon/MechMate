using System.Collections.ObjectModel;
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
    public async Task<bool> WriteJsonAsync<T>(ObservableCollection<T> newData, string fileName)
    {
        try
        {
            // Get the app's local data directory
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            string jsonDocument = JsonSerializer.Serialize(newData);
            await File.WriteAllTextAsync(filePath, jsonDocument);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error writing JSON: {ex.Message}");
            return false;
        }
    }

    // Read JSON list data from file
    public async Task<ObservableCollection<T>?> ReadJsonListAsync<T>(string fileName) where T : class
    {
        try
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            string[] fileStringArray = await File.ReadAllLinesAsync(filePath);
            //string fileString = string.Concat(fileStringArray);
            string fileString = string.Join(string.Empty, fileStringArray);
            //var json = await FileSystem.OpenAppPackageFileAsync(fileName);
            var jsonDocument = JsonDocument.Parse(fileString);
            return JsonSerializer.Deserialize<ObservableCollection<T>>(jsonDocument);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error reading JSON: {ex.Message}");
            return new ObservableCollection<T>();
        }
    }
}