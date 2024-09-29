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

            //-----Data inicial para poder realizar las pruebas

            modelBuilder.Entity<Rol>().HasData(
            new Rol { IdRol = 1, name = "ADMIN"},
            new Rol { IdRol = 2, name = "EMPLEADO"});


            modelBuilder.Entity<User>().HasData(
            new User { IdUser = 1, Name = "alejandro", Email = "alejrando@gmail.com", IdRol=1, Password= "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", CreatedDate=DateTime.Now, IsActive = 1},
            new User { IdUser = 2, Name = "miguel",  Email = "miguel@gmail.com", IdRol = 2, Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", CreatedDate = DateTime.Now, IsActive = 1});


            modelBuilder.Entity<Category>().HasData(
            new Category { IdCategoria = 1, Name = "Autos" },
            new Category { IdCategoria = 2, Name = "Motocicletas" });


            modelBuilder.Entity<Product>().HasData(
            new Product { IdProduct = 1, Name = "MT-15", IdCategoria=2, DatedCreate=DateTime.Now, Price=16000000, Description="De la linea deportiva", Stock=2},
            new Product { IdProduct = 2, Name = "Mazda 3", IdCategoria = 1, DatedCreate = DateTime.Now, Price = 25000000, Description = "un auto de excelente gama", Stock = 2 });

        }

    }
}
