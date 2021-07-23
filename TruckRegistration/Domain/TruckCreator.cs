using TruckRegistration.Models;

namespace TruckRegistration.Domain
{
    public class TruckCreator : Truck
    {
        public TruckCreator(string model, int productionYear, int? modelYear = null)
        {
            Model = model;
            ProductionYear = productionYear;
            ModelYear = modelYear ?? productionYear;
        }

        public TruckCreator(int id, string model, int productionYear, int? modelYear = null)
        {
            Id = id;
            Model = model;
            ProductionYear = productionYear;
            ModelYear = modelYear ?? productionYear;
        }
    }
}
