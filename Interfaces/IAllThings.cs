using shhop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.Interfaces
{
    public interface IAllThings<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<Thing> Things { get; }
        Thing getObjectThing(int thingId);

        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Create(TEntity entity);
        void Save();
    }
}

