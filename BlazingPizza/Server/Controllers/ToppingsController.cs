using System;
using BlazingPizza.Server.Models;
using BlazingPizza.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace BlazingPizza.Server.Controllers
{
    [Route("toppings")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaStoreContext Context;

        public ToppingsController(PizzaStoreContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetToppings()
        {
            return await Context.Toppings.OrderBy(t => t.Name).ToListAsync();
        }
    }
}