using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;
using Microsoft.Maui.Controls;

namespace MechMate.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Engine { get; set; } = string.Empty;
        public string VIN { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BodyType { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new List<string>();
        public string OwnerId { get; set; } = string.Empty;
        public string ImageBase64 { get; set; } = string.Empty;

        [JsonIgnore]
        [BsonIgnore]
        public string DisplayName => $"{Year} {Make} {Model}";

        // Add this property for XAML binding
        [JsonIgnore]
        [BsonIgnore]
        public ImageSource? ImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImageBase64))
                    return null;
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(ImageBase64);
                    return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
