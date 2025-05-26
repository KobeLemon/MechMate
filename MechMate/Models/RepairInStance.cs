using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MechMate.Models
{
    class RepairInstance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RepairId { get; set; } = ObjectId.GenerateNewId().ToString();
        public string VehicleId { get; set; } = string.Empty;
        public DateTime RepairDate { get; set; }
        public Dictionary<string, string> RepairNotes { get; set; } = new Dictionary<string, string>();
        public int RepairMileage { get; set; }
        public List<string> ToolsUsed { get; set; } = new List<string>();
        public List<RepairPart> PartsUsed { get; set; } = new List<RepairPart>();
    }
}
