namespace MechMate.Models
{
    class RepairInstance
    {
        public int RepairId { get; set; }
        public string VehicleId { get; set; }
        public DateTime RepairDate { get; set; }
        public Dictionary<string, string> RepairNotes { get; set; }
        public int RepairMileage { get; set; }
        public List<string> ToolsUsed { get; set; }
    }
}
