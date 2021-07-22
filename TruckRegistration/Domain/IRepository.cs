using System;
using System.Collections.Generic;
using TruckRegistration.Models;

namespace TruckRegistration.Domain
{
    interface IRepository<T> : IDisposable where T : Entity
    {
        T Add(T item);

        void Delete(int id);

        void Edit(T item);

        bool Exists(int id);

        T Get(int id);

        List<T> GetAll();
    }
}
