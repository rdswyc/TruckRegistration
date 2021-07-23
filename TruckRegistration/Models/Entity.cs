namespace TruckRegistration.Models
{
    /// <summary>
    /// Base class for all entities in the project that have an Id property
    /// </summary>
    public abstract class Entity
    {
        int _id;

        /// <summary>
        /// This Id property can only be modified by the inheriting class, not from outside.
        /// </summary>
        public virtual int Id
        {
            get => _id;
            protected set => _id = value;
        }
    }
}
