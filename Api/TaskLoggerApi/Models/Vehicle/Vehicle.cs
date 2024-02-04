namespace Api.Models.Vehicle
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

        public double TaxLevy
        {
            get
            {
                double taxRate = 1.0;

                if (this.FuelType.Equals("Petrol", StringComparison.OrdinalIgnoreCase))
                {
                    taxRate = 1.2; // Petrol vehicles have 20% higher tax
                }

                switch (this.Type.ToLower())
                {
                    case "car":
                        return 1500 * taxRate;
                    case "bike":
                        return 1000; 
                    case "plane":
                        return 5000;
                    case "boat":
                        return 2000 * (this.FuelType.Equals("Petrol", StringComparison.OrdinalIgnoreCase) ? 1.15 : 1);
                    default:
                        return 0;
                }
            }
        }
        public string GetRoadworthyTestInterval()
        {
            switch (this.Type.ToLower())
            {
                case "car":
                    return Year < DateTime.Now.Year - 10 ? "every year" : "every 2 years";
                case "bike":
                    return Year > DateTime.Now.Year - 5 ? "every 6 months" : null;
                case "boat":
                case "plane":
                    return null;
                default:
                    return "no roadworthy test applies";
            }
        }

    }

}
