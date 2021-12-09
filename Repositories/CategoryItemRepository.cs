using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shhop.DB;
using shhop.Interfaces;
using shhop.Models;

namespace shhop.Repositories
{
    public class CategoryItemRepository : IAllCategoryItems<CategoryItem>
    {
        private readonly ApplicationContext context;
        private readonly DbSet<CategoryItem> categoryItems;

        public CategoryItemRepository(ApplicationContext context, DbSet<CategoryItemRepository> categoryItems)
        {
            this.context = context;
            this.categoryItems = this.context.CategoryItems;
        }

        public IEnumerable<CategoryItem> CategoryItems => context.CategoryItems;

        public void Create(CategoryItem entity)
        {
            context.CategoryItems.Add(entity);
        }

        public void Delete(int id)
        {
            var categoryItem = context.CategoryItems.Find(id);
            Delete(categoryItem);
        }

        public void Delete(CategoryItem entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.CategoryItems.Attach(entity);
            }
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CategoryItem getObjectCategoryItem(int categoryItemId)
        {
            return context.CategoryItems.FirstOrDefault(p => p.id == categoryItemId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(CategoryItem entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
