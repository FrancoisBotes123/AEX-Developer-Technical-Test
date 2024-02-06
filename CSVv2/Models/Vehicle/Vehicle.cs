namespace CSVv2.Models.Vehicle
{
    public class Vehicle
    {
        public string Type { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int? WheelCount { get; set; }
        public string FuelType { get; set; }
        public bool IsActive { get; set; }

        public double TaxLevy { get;set;}
        public string Roadworthy { get; set; }

    }
}
