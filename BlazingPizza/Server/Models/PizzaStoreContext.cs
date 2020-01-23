﻿using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TIW1004\SQLEXPRESS;Database=PizzaStore; trusted_Connection=True;",
            providerOptions => providerOptions.CommandTimeout(300));
        }
    }
}
