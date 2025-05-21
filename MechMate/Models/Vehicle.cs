using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Mechmate.Models
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

        [JsonIgnore]
        [BsonIgnore]
        public string DisplayName => $"{Year} {Make} {Model}";

        
    }
}
