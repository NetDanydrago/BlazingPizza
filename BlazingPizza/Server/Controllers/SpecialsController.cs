﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazingPizza.Server.Models;
using BlazingPizza.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Server.Controllers
{
    [Route("specials")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private readonly PizzaStoreContext Context;
        public SpecialsController(PizzaStoreContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecial() 
        {
            return (await Context.Specials.ToListAsync())
                 .OrderByDescending(s => s.BasePrice).ToList();
        }
    }
}
