﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazingPizza.Shared
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; } = new Address();
       
        public LatLong DeliveryLocation { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public String GetFormattedTotalPrice() =>
            GetTotalPrice().ToString("0.00");

        public string GetFormattedCreatedTime() => CreatedTime.ToString("D", new System.Globalization.CultureInfo("es"));
    }
}
