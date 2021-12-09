using shhop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shhop.Interfaces
{
    public interface IAllCategories<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<Category> Categories { get; }
        Category getObjectCategory(int categoryId);

        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Create(TEntity entity);
        void Save();
    }
}
