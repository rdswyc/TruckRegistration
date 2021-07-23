namespace TruckRegistration.Domain
{
    public class TruckViewModel
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int ProductionYear { get; set; }

        public int ModelYear { get; set; }

        public override string ToString()
        {
            return $"Truck {Id}: {Model} {ProductionYear}/{ModelYear}";
        }
    }
}
