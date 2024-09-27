using PruebaAPI.MODEL;
using Microsoft.EntityFrameworkCore;

namespace PruebaAPI.DAL.DBContext
{
    public class MyDbContext: DbContext 
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
       : base(options){}

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<Rol> Rol { get; set; }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
