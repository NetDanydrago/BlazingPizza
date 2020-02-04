using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Shared;

namespace BlazingPizza.Client.Services
{
    public class OrderState
    {
        public Pizza ConfiguringPizza { get; private set; }
        public bool ShowConfigureDialog { get; private set; }
        public Order Order { get; private set; } = new Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowConfigureDialog = true;
        }

       public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowConfigureDialog = false;
        }

      public  void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;

            ShowConfigureDialog = false;
        }


       public void removeConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            Order = new Order();
        }
    }
}
