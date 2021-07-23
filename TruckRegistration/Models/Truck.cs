﻿namespace TruckRegistration.Models
{
    public class Truck : Entity
    {
        public string Model { get; set; }

        public int ProductionYear { get; set; }

        public int ModelYear { get; set; }

        public override string ToString()
        {
            return $"Truck {Id}: {Model} {ProductionYear}/{ModelYear}";
        }
    }
}
