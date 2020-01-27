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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TIW1004\SQLEXPRESS;Database=PizzaStore; trusted_Connection=True;",
            providerOptions => providerOptions.CommandTimeout(300));
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
        }
    }
}
