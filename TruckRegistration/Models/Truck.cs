namespace TruckRegistration.Models
{
    /// <summary>
    /// The main Truck aggregator entity for this project.
    /// It is used as a base class for the Truck table in the database, after the configurations.
    /// It can be extended by adding more properties or owned entity types
    /// </summary>
    public class Truck : Entity
    {
        /// <summary>
        /// Truck Model. In the database it will be mapped to as a 50 character string for future extention.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Truck Production Year. In the database it will be mapped as a smallint.
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Truck Model Year. In the database it will be mapped as a smallint.
        /// </summary>
        public int ModelYear { get; set; }

        /// <summary>
        /// A method to produce a readable string output to the model, eg 'Truck FM 2020/2021'
        /// </summary>
        public override string ToString()
        {
            return $"Truck {Id}: {Model} {ProductionYear}/{ModelYear}";
        }
    }
}
