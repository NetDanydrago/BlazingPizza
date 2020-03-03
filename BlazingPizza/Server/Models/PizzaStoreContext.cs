using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Shared;

namespace BlazingPizza.Server.Models
{
    public class PizzaStoreContext : DbContext
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=TIW1004\SQLEXPRESS;Database=PizzaStore; trusted_Connection=True;",
            optionsBuilder.UseSqlite("Data Source=PizzaStore.db");
            //providerOptions => providerOptions.CommandTimeout(300));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Definir la llave primaria de la entidad pizzaTopping
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(pst => new { pst.PizzaId, pst.ToppingId });
            //Una Pizza puede Tener MUCHOS Toppings.
            modelBuilder.Entity<PizzaTopping>()
                .HasOne<Pizza>().WithMany(ps => ps.Toppings);
            // Un Topping Puede Estar en Muchas Pizzas.
            modelBuilder.Entity<PizzaTopping>()
                .HasOne(pst => pst.Topping).WithMany();
            // Definir LatLong como un Owned Entity Type de Order.
            // Con esto las propiedades de la entidad LatLong se crearán en la tabla Order
            // en lugar de crear una nueva tabla y una llave foránea para relacionarlas.
            modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
        }
    }
}
