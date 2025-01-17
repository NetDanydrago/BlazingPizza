﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlazingPizza.Shared
{
   public class Pizza
    {
        public const int DefaultSize = 30;
        public const int MinimunSize = 20;
        public const int MaximunSize = 40;
        public const int IncrementSize = 2;

        public int Id { get; set; }
        public int OrderId { get; set; }
        public PizzaSpecial Special { get; set; }
        public int SpecialId { get; set; }
        public int Size { get; set; }
        public List<PizzaTopping> Toppings { get; set; }
        public decimal GetBasePrice() => ((decimal)Size/(decimal)DefaultSize) * Special.BasePrice;
        public decimal GetTotalPrice()
        {
            return GetBasePrice() + Toppings.Sum(t => t.Topping.Price);
        }
        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
