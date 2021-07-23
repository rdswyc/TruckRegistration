using System;
using System.Collections.Generic;
using System.Linq;
using TruckRegistration.Domain;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    /// <summary>
    /// Following the repository pattern, this generic class allows all the basic operations on the database.
    /// Notice it can be implemented by multiple different model classes, if they are registered in the DbContext.
    /// </summary>
    /// <typeparam name="T">The model class mapped to the DbContext.</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private bool disposedValue;
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Generic implementation to add an item to the database.
        /// </summary>
        /// <param name="item">The item of type T to be added</param>
        /// <returns>The actual item added to the database</returns>
        public T Add(T item)
        {
            var entry = _context.Set<T>().Add(item);
            _context.SaveChanges();
            return entry.Entity;
        }

        /// <summary>
        /// Generic implementation to delete an item from the database, using it's id.
        /// </summary>
        /// <param name="id">The id of the item to find and delete.</param>
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id) ?? throw new KeyNotFoundException();
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Generic implementation to edit an item from the database. The id must be of an existing item.
        /// </summary>
        /// <param name="item">The item to edit.</param>
        public void Edit(T item)
        {
            if (!Exists(item.Id))
                throw new KeyNotFoundException();

            _context.Set<T>().Update(item);
            _context.SaveChanges();
        }

        /// <summary>
        /// Implementation to indicate if an item with the specified id exists on the database.
        /// </summary>
        /// <param name="id">The id of the item to find.</param>
        /// <returns>True if the item exists, false otherwise.</returns>
        public bool Exists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        /// <summary>
        /// Generic implementation to return an item from the database, base on it's id.
        /// </summary>
        /// <param name="id">The id of the item to get.</param>
        /// <returns>The corresponding item of type T</returns>
        public T Get(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        /// <summary>
        /// Generic implementation to return all items from the database.
        /// </summary>
        /// <returns>A list of all items of type T</returns>
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Clean up method to avoid memory leaks from the context.
        /// </summary>
        /// <param name="disposing">A flag to indicate if the class is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
