using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace shhop.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Thing> Thing { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
