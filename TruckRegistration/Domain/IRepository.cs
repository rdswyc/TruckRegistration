using System;
using System.Collections.Generic;
using TruckRegistration.Models;

namespace TruckRegistration.Domain
{
    /// <summary>
    /// Using the adapters patter and persistency ignorance, this generic interface set's the contract to basic database operations.
    /// Classes using this contract should also implement the Dispose method to free up unmanaged resources.
    /// </summary>
    /// <typeparam name="T">The type of entity to be mapped to a table in the database.</typeparam>
    public interface IRepository<T> : IDisposable where T : Entity
    {
        /// <summary>
        /// Generic method to add an item to the database.
        /// </summary>
        /// <param name="item">The item of type T to be added</param>
        /// <returns>The actual item added to the database</returns>
        T Add(T item);

        /// <summary>
        /// Generic method to delete an item from the database, using it's id.
        /// </summary>
        /// <param name="id">The id of the item to find and delete.</param>
        void Delete(int id);

        /// <summary>
        /// Generic method to edit an item from the database. The id must be of an existing item.
        /// </summary>
        /// <param name="item">The item to edit.</param>
        void Edit(T item);

        /// <summary>
        /// Method to indicate if an item with the specified id exists on the database.
        /// </summary>
        /// <param name="id">The id of the item to find.</param>
        /// <returns>True if the item exists, false otherwise.</returns>
        bool Exists(int id);

        /// <summary>
        /// Generic method to return an item from the database, base on it's id.
        /// </summary>
        /// <param name="id">The id of the item to get.</param>
        /// <returns>The corresponding item of type T</returns>
        T Get(int id);

        /// <summary>
        /// Generic method to return all items from the database.
        /// </summary>
        /// <returns>A list of all items of type T</returns>
        IEnumerable<T> GetAll();
    }
}
