namespace TruckRegistration.Models
{
    public abstract class Entity
    {
        int _id;

        public virtual int Id
        {
            get => _id;
            protected set => _id = value;
        }
    }
}
