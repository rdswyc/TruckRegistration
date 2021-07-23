using TruckRegistration.Models;

namespace TruckRegistration.Domain
{
    /// <summary>
    /// This helper class has two goals:
    ///   - Allow for setting the protected Id of the Truck entity
    ///   - Ease the creation of a Truck entity by passing it's values to the constructors.
    /// </summary>
    public class TruckCreator : Truck
    {
        /// <summary>
        /// Creator version without the Id.
        /// </summary>
        /// <param name="model">The truck model.</param>
        /// <param name="productionYear">The truck production year.</param>
        /// <param name="modelYear">The truck model year. If missing, will be set to the production year.</param>
        public TruckCreator(string model, int productionYear, int? modelYear = null)
        {
            Model = model;
            ProductionYear = productionYear;
            ModelYear = modelYear ?? productionYear;
        }

        /// <summary>
        /// Version including the protected Id field.
        /// </summary>
        /// <param name="id">The id of the truck entity.</param>
        /// <param name="model">The truck model.</param>
        /// <param name="productionYear">The truck production year.</param>
        /// <param name="modelYear">The truck model year. If missing, will be set to the production year.</param>
        public TruckCreator(int id, string model, int productionYear, int? modelYear = null)
        {
            Id = id;
            Model = model;
            ProductionYear = productionYear;
            ModelYear = modelYear ?? productionYear;
        }
    }
}
