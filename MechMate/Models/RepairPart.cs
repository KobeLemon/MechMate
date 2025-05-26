namespace MechMate.Models
{
    class RepairPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string StoreName { get; set; }
        public double PartPrice { get; set; }
        public int Quantity { get; set; }
    }
}
