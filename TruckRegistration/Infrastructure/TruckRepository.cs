using System;
using System.Collections.Generic;
using System.Linq;
using TruckRegistration.Domain;
using TruckRegistration.Models;

namespace TruckRegistration.Infrastructure
{
    public class TruckRepository : IRepository<Truck>
    {
        private bool disposedValue;
        private readonly TruckContext _context;

        public TruckRepository(TruckContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Truck Add(Truck item)
        {
            var Truck = _context.Set<Truck>().Add(item);
            _context.SaveChanges();
            return Truck.Entity;
        }

        public void Delete(int id)
        {
            var Truck = _context.Set<Truck>().Find(id) ?? throw new KeyNotFoundException();
            _context.Set<Truck>().Remove(Truck);
            _context.SaveChanges();
        }

        public void Edit(Truck item)
        {
            var Truck = _context.Set<Truck>().Find(item.Id) ?? throw new KeyNotFoundException();
            _context.Set<Truck>().Update(Truck);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Set<Truck>().Any(e => e.Id == id);
        }

        public Truck Get(int id)
        {
            var Truck = _context.Set<Truck>().Find(id);
            return Truck;
        }

        public List<Truck> GetAll()
        {
            return _context.Set<Truck>().ToList();
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
