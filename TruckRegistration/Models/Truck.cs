namespace TruckRegistration.Models
{
    public class Truck : Entity
    {
        public string Model { get; set; }

        public int ProductionYear { get; set; }

        public int ModelYear { get; set; }
    }
}
