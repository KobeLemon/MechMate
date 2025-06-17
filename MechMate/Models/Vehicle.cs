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
        public string Make { get; set; } = "none";
        public string Model { get; set; } = "none";
        public int Year { get; set; } = 1900;
        public string Engine { get; set; } = "none";
        public string VIN { get; set; } = "none";
        public string PlateNumber { get; set; } = "none";
        public string Color { get; set; } = "none";
        public string BodyType { get; set; } = "none";
        public string FuelType { get; set; } = "none";
        public string Transmission { get; set; } = "none";
        public List<string> Features { get; set; } = new List<string>();
        public string OwnerId { get; set; } = "none";
        public string ImageBase64 { get; set; } = "none";

        [JsonIgnore]
        [BsonIgnore]
        public string DisplayName => $"{Year} {Make} {Model}";

        //// Add this property for XAML binding
        //[JsonIgnore]
        //[BsonIgnore]
        //public ImageSource? ImageUrl
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(ImageBase64))
        //            return null;
        //        try
        //        {
        //            byte[] imageBytes = Convert.FromBase64String(ImageBase64);
        //            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

     // add this property for XAML binding
        [JsonIgnore]
        [BsonIgnore]
        public ImageSource? ImageUrl { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public List<DisplayItem> DisplayVehicle => new List<DisplayItem>
            {
                new() {Key = "Year", Value = Year.ToString()},
                new() {Key = "Make", Value = Make},
                new() {Key = "Model", Value = Model},
                new() {Key = "Engine", Value = Engine},
                new() {Key = "VIN", Value = VIN},
                new() {Key = "Plate Number", Value = PlateNumber},
                new() {Key = "Color", Value = Color},
                new() {Key = "Body Type", Value = BodyType},
                new() {Key = "Fuel Type", Value = FuelType},
                new() {Key = "Transmission", Value = Transmission}
            }.Concat(Features.Select(feature => new DisplayItem { Key = "Feature", Value = feature })).ToList();
    }
}
