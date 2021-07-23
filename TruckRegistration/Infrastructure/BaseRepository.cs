using System;
using System.Collections.Generic;
using System.Linq;
using TruckRegistration.Domain;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private bool disposedValue;
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T Add(T item)
        {
            var entry = _context.Set<T>().Add(item);
            _context.SaveChanges();
            return entry.Entity;
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id) ?? throw new KeyNotFoundException();
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Edit(T item)
        {
            var entity = _context.Set<T>().Find(item.Id) ?? throw new KeyNotFoundException();
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        public T Get(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

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
